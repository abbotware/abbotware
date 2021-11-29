// -----------------------------------------------------------------------
// <copyright file="AbbotwareShellCommand{TOptions}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.ShellCommand
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reactive.Subjects;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Helpers;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using Abbotware.ShellCommand.Configuration;

    /// <summary>
    /// class the can run a shell command
    /// </summary>
    /// <typeparam name="TOptions">options type</typeparam>
    public partial class AbbotwareShellCommand<TOptions> : BaseCommand<TOptions, IExitInfo>, IShellCommand
        where TOptions : class, IShellCommandOptions
    {
        private readonly Subject<(DateTimeOffset Time, string Message)> standardOutput = new();

        private readonly Subject<(DateTimeOffset Time, string Message)> standardError = new();

        private readonly Subject<string> standardInput = new();

        private readonly TaskCompletionSource<IStartInfo> startSignal = new();

        private readonly TaskCompletionSource<IExitInfo> exitSignal = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="AbbotwareShellCommand{TShellCommandOptions}"/> class.
        /// </summary>
        /// <param name="config">configuration</param>
        /// <param name="logger">injected logger</param>
        public AbbotwareShellCommand(TOptions config, ILogger logger)
            : base(config, logger)
        {
        }

        /// <inheritdoc/>
        public IObservable<(DateTimeOffset Time, string Message)> StandardOutput => this.standardOutput;

        /// <inheritdoc/>
        public IObservable<(DateTimeOffset Time, string Message)> ErrorOutput => this.standardError;

        /// <inheritdoc/>
        public Task<IStartInfo> Started => this.startSignal.Task;

        /// <inheritdoc/>
        public Task<IExitInfo> Exited => this.exitSignal.Task;

        /// <inheritdoc/>
        public void WriteInput(string input)
        {
            this.standardInput.OnNext(input);
        }

        /// <inheritdoc/>
        protected override async Task<IExitInfo> OnExecuteAsync(CancellationToken ct)
        {
            var r = new ExitInfo();

            using var outputSubscription = this.StandardOutput.Subscribe(r.AppendOutput);
            using var errortSubscription = this.ErrorOutput.Subscribe(r.AppendError);

            try
            {
                using var process = new Process();

                process.StartInfo.FileName = this.Configuration.Command;

                if (this.Configuration.Arguments != null)
                {
                    process.StartInfo.Arguments = this.Configuration.Arguments;
                }

                if (this.Configuration.WorkingDirectory.IsNotBlank())
                {
                    process.StartInfo.WorkingDirectory = this.Configuration.WorkingDirectory;

                    var target = Path.Combine(process.StartInfo.WorkingDirectory, process.StartInfo.FileName);

                    if (!File.Exists(target))
                    {
                        throw new FileNotFoundException("shell command not found", target);
                    }
                }

                process.StartInfo.ErrorDialog = false;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                if (!PlatformHelper.IsUnix)
                {
                    process.StartInfo.LoadUserProfile = false;
                }

                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true;
                process.EnableRaisingEvents = true;

                process.OutputDataReceived += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(e.Data))
                    {
                        return;
                    }

                    this.standardOutput.OnNext((DateTimeOffset.Now, this.MaskData(e.Data)));
                };

                process.ErrorDataReceived += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(e.Data))
                    {
                        return;
                    }

                    this.standardError.OnNext((DateTimeOffset.Now, this.MaskData(e.Data)));
                };

                var shellCommandTcs = new TaskCompletionSource<bool>();

                process.Exited += (s, e) =>
                {
                    this.Logger.Debug("exited");
                    shellCommandTcs.SetResult(true);
                };

                var logCommand = GenerateLogInfo(process);

                this.Logger.Debug($"Start:'{logCommand}'");

                r.StartInfo.Started = process.Start();

                // Didn't start so return immediately
                if (!r.StartInfo.Started)
                {
                    return r;
                }

                using var stdInSub = this.ProcessStarted(r, process);

                this.OnStarted()
                    .Forget(this.Logger);

                try
                {
                    await shellCommandTcs.Task.TimeoutAfter(this.Configuration.CommandTimeout, ct)
                        .ConfigureAwait(false);

                    process.Refresh();

                    // minor delay to ensure OutputDataReceived / ErrorDataReceived callback events are finished
#if NET6_0_OR_GREATER
                    using var waitForExitCt = new CancellationTokenSource(this.Configuration.ExitDelay);

                    await process.WaitForExitAsync(waitForExitCt.Token)
                        .ConfigureAwait(false);

                    r.Exited = true;
#else
                    r.Exited = process.WaitForExit((int)this.Configuration.ExitDelay.TotalMilliseconds);
#endif
                    r.ExitCode = process.ExitCode;
                }
                catch (TimeoutException)
                {
                    this.Logger.Warn($"Timeout:'{logCommand}' ProcessId:{process.Id}");
                    this.ProcessCleanup(r, process);
                }
            }
            finally
            {
                r.End = DateTimeOffset.Now;
                this.Logger.Debug(r.ToString());
            }

            this.exitSignal.SetResult(r);

            return r;
        }

        /// <summary>
        /// Logic hook for after started;
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        protected virtual Task OnStarted()
        {
            return Task.CompletedTask;
        }

        private static string GenerateLogInfo(Process process)
        {
            if (string.IsNullOrWhiteSpace(process.StartInfo.WorkingDirectory))
            {
                return $"Command: '{process.StartInfo.FileName} {process.StartInfo.Arguments}'";
            }

            return $"Command: '{process.StartInfo.WorkingDirectory}{Path.DirectorySeparatorChar}{process.StartInfo.FileName} {process.StartInfo.Arguments}'";
        }

        private IDisposable ProcessStarted(ExitInfo r, Process process)
        {
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            r.StartInfo.ProcessId = process.Id;

            var sub = this.standardInput.Subscribe(process.StandardInput.WriteLine);

            this.startSignal.SetResult(r.StartInfo);

            return sub;
        }

        private async void ProcessCleanup(ExitInfo r, Process process)
        {
            process.Refresh();

            process.CancelErrorRead();
            process.CancelOutputRead();

            r.Exited = process.HasExited;

            // process exited?, can skip killing it
            if (r.Exited)
            {
                r.ExitCode = process.ExitCode;
                return;
            }

            try
            {
                process.Kill();

                await Task.Delay(this.Configuration.ExitDelay)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.Logger.Warn(ex, $"kill process:{r.StartInfo.ProcessId}");
            }
        }
    }
}