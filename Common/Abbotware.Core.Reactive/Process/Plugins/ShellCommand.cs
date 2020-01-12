// -----------------------------------------------------------------------
// <copyright file="ShellCommand.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Process.Plugins
{
    using System;
    using System.Diagnostics;
    using System.Reactive.Subjects;
    using System.Threading.Tasks;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Helpers;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using Abbotware.Core.Process.Configuration;
    using Abbotware.Core.Process.Configuration.Models;

    /// <summary>
    /// class the can run a shell command
    /// </summary>
    public class ShellCommand : BaseCommand<IShellCommandOptions, IShellCommandResult>, IShellCommand
    {
        private readonly Subject<(DateTimeOffset, string)> standardOutput = new Subject<(DateTimeOffset, string)>();

        private readonly Subject<(DateTimeOffset, string)> standardError = new Subject<(DateTimeOffset, string)>();

        private readonly Subject<string> standardInput = new Subject<string>();

        private readonly TaskCompletionSource<IShellCommandStartInfo> startSignal = new TaskCompletionSource<IShellCommandStartInfo>();

        private readonly TaskCompletionSource<IShellCommandResult> exitSignal = new TaskCompletionSource<IShellCommandResult>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellCommand"/> class.
        /// </summary>
        /// <param name="command">command</param>
        /// <param name="logger">injected logger</param>
        public ShellCommand(string command, ILogger logger)
            : this(command, string.Empty, logger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellCommand"/> class.
        /// </summary>
        /// <param name="command">command</param>
        /// <param name="arguments">arguments</param>
        /// <param name="logger">injected logger</param>
        public ShellCommand(string command, string arguments, ILogger logger)
            : this(new ShellCommandOptions(command, arguments), logger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellCommand"/> class.
        /// </summary>
        /// <param name="config">configuration</param>
        /// <param name="logger">injected logger</param>
        public ShellCommand(IShellCommandOptions config, ILogger logger)
            : base(config, logger)
        {
        }

        /// <inheritdoc/>
        public IObservable<(DateTimeOffset, string)> StandardOutput => this.standardOutput;

        /// <inheritdoc/>
        public IObservable<(DateTimeOffset, string)> ErrorOutput => this.standardError;

        /// <inheritdoc/>
        public Task<IShellCommandStartInfo> Started => this.startSignal.Task;

        /// <inheritdoc/>
        public Task<IShellCommandResult> Exited => this.exitSignal.Task;

        /// <inheritdoc/>
        public void WriteInput(string input)
        {
            this.standardInput.OnNext(input);
        }

        /// <inheritdoc/>
        protected override async Task<IShellCommandResult> OnExecuteAsync()
        {
            var r = new ShellCommandResult();

            using var outputSubscription = this.StandardOutput.Subscribe(r.AppendOutput);
            using var errortSubscription = this.StandardOutput.Subscribe(r.AppendError);

            try
            {
                using var process = new Process();

                process.StartInfo.FileName = this.Configuration.Command;

                if (this.Configuration.Arguments != null)
                {
                    process.StartInfo.Arguments = this.Configuration.Arguments;
                }

                if (this.Configuration.WorkingDirectory != null)
                {
                    process.StartInfo.WorkingDirectory = this.Configuration.WorkingDirectory;
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

                    this.standardOutput.OnNext((DateTimeOffset.Now, e.Data));
                };

                process.ErrorDataReceived += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(e.Data))
                    {
                        return;
                    }

                    this.standardError.OnNext((DateTimeOffset.Now, e.Data));
                };

                var tcs = new TaskCompletionSource<bool>();

                process.Exited += (s, e) =>
                {
                    this.Logger.Debug("exited");
                    tcs.SetResult(true);
                };

                var logCommand = this.GenerateLogInfo(process);

                this.Logger.Debug($"Start:'{logCommand}'");

                r.StartInfo.Started = process.Start();

                // Didn't start so return immediately
                if (!r.StartInfo.Started)
                {
                    return r;
                }

                this.ProcessStarted(r, process);

                try
                {
                    await tcs.Task.TimeoutAfter(this.Configuration.CommandTimeout)
                        .ConfigureAwait(false);

                    // minor delay to ensure OutputDataReceived / ErrorDataReceived callback events are finished
                    await Task.Delay(this.Configuration.ExitDelay)
                        .ConfigureAwait(false);

                    process.Refresh();

                    r.Exited = true;
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

        private string GenerateLogInfo(Process process)
        {
            if (string.IsNullOrWhiteSpace(process.StartInfo.WorkingDirectory))
            {
                return $"Command: '{process.StartInfo.FileName} {process.StartInfo.Arguments}'";
            }

            return $"Command: '{process.StartInfo.WorkingDirectory}/{process.StartInfo.FileName} {process.StartInfo.Arguments}'";
        }

        private IDisposable ProcessStarted(ShellCommandResult r, Process process)
        {
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            r.StartInfo.ProcessId = process.Id;

            var sub = this.standardInput.Subscribe(process.StandardInput.WriteLine);

            this.startSignal.SetResult(r.StartInfo);

            return sub;
        }

        private async void ProcessCleanup(ShellCommandResult r, Process process)
        {
            process.Refresh();

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