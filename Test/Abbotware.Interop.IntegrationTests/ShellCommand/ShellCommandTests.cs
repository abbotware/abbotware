// <copyright file="ShellCommandTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>

namespace Abbotware.IntegrationTests.Core
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Abbotware.ShellCommand;
    using Abbotware.ShellCommand.Configuration.Models;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("ShellCommand")]
    public class ShellCommandTests : BaseNUnitTest
    {
        [Test]
        [MaxTime(10000)]
        public async Task ShellCommand_Ftp_ExecuteAsync()
        {
            Assert.Inconclusive();

            IExitInfo result = null;

            // command will exit before kill is issued
            var cfg = CreateCommandConfig(4, TimeSpan.FromSeconds(10));

            using (var child = new AbbotwareShellCommand(cfg, this.Logger))
            {
                result = await child.ExecuteAsync(default);

                Assert.IsTrue(result.StartInfo.Started);
                Assert.IsTrue(result.StartInfo.ProcessId > 0);

                Assert.IsTrue(result.Exited);
                Assert.AreEqual(0, result.ExitCode);

                Assert.IsTrue(child.Started.IsCompleted);
                Assert.IsTrue(child.Exited.IsCompleted);

                Assert.AreSame(result, child.Exited.Result);
            }

            Assert.That(result.ErrorOutput, Has.Count.GreaterThan(1));
            Assert.That(result.StandardOutput, Has.Count.GreaterThan(1));

            try
            {
                using var p = Process.GetProcessById(result.StartInfo.ProcessId.Value);

                Assert.IsNull(p);
            }
            catch (Exception)
            {
                return;
            }

            Assert.Fail("Child process should have exited");
        }

        [Test]
        [MaxTime(10000)]
        public async Task ShellCommand_Ftp_ExecuteAsync_Kill()
        {
            Assert.Inconclusive();

            IExitInfo result = null;

            // timeout is less than number of pings should force a kill
            var cfg = CreateCommandConfig(10, TimeSpan.FromSeconds(4));

            using (var child = new AbbotwareShellCommand(cfg, this.Logger))
            {
                result = await child.ExecuteAsync(default);

                Assert.IsTrue(result.StartInfo.Started);
                Assert.IsTrue(result.StartInfo.ProcessId > 0);

                Assert.IsFalse(result.Exited);
                Assert.IsNull(result.ExitCode);

                Assert.IsTrue(child.Started.IsCompleted);
            }

            await Task.Delay(1000);

            try
            {
                using var p = Process.GetProcessById(result.StartInfo.ProcessId.Value);
                Assert.IsNull(p);
            }
            catch (Exception)
            {
                return;
            }

            Assert.Fail("Child process should have been killed");
        }

        private static ShellCommandOptions CreateCommandConfig(int count, TimeSpan timeout)
        {
            string cmd = "ftp.exe";

            if (IsLinux())
            {
                cmd = "ftp";
            }

            var host = Environment.GetEnvironmentVariable("UNITTEST_FTP_HOST");
            ////var user = Environment.GetEnvironmentVariable("UNITTEST_FTP_USERNAME");
            ////var pass = Environment.GetEnvironmentVariable("UNITTEST_FTP_PASSWORD");

            var args = $" -i -s test.txt {host}";

            if (IsLinux())
            {
                args = $"localhost -c {count}";
            }

            var cfg = new ShellCommandOptions(cmd, args)
            {
                CommandTimeout = timeout,
            };

            return cfg;
        }
    }
}