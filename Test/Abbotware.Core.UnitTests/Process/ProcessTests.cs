﻿// <copyright file="ProcessTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Abbotware.Interop.NUnit;
    using Abbotware.ShellCommand;
    using Abbotware.ShellCommand.Configuration.Models;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Core")]
    [Category("Core.Process")]
    public class ProcessTests : BaseNUnitTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateChildProcessContractFailure()
        {
            _ = new ShellCommandOptions(string.Empty, string.Empty);
        }

        [Test]
        [MaxTime(10000)]
        public async Task ShellCommand_Ping_ExecuteAsync()
        {
            IExitInfo result = null;

            // command will exit before kill is issued
            var cfg = CreateCommandConfig(4, TimeSpan.FromSeconds(10));

            using (var child = new AbbotwareShellCommand(cfg, this.Logger))
            {
                result = await child.ExecuteAsync();

                Assert.IsTrue(result.StartInfo.Started);
                Assert.IsTrue(result.StartInfo.ProcessId > 0);

                Assert.IsTrue(result.Exited);
                Assert.AreEqual(0, result.ExitCode);

                Assert.IsTrue(child.Started.IsCompleted);
                Assert.IsTrue(child.Exited.IsCompleted);

                Assert.AreSame(result, child.Exited.Result);
            }

            Assert.That(result.ErrorOutput, Has.Count.GreaterThan(1));

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
        public async Task ShellCommand_Ping_ExecuteAsync_Kill()
        {
            IExitInfo result = null;

            // timeout is less than number of pings should force a kill
            var cfg = CreateCommandConfig(10, TimeSpan.FromSeconds(4));

            using (var child = new AbbotwareShellCommand(cfg, this.Logger))
            {
                result = await child.ExecuteAsync();

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
            string cmd = "ping.exe";

            if (IsLinux())
            {
                cmd = "ping";
            }

            string args = $"localhost -n {count}";

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