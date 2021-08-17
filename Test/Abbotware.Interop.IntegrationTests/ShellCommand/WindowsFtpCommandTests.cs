// <copyright file="WindowsFtpCommandTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>

namespace Abbotware.IntegrationTests.Core
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Abbotware.ShellCommand;
    using Abbotware.ShellCommand.Plugins;
    using Abbotware.ShellCommand.Plugins.Configuration.Models;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("ShellCommand")]
    public class WindowsFtpCommandTests : BaseNUnitTest
    {
        [Test]
        public void WindowsFtpCommand_ArgumentsRender()
        {
            // command will exit before kill is issued
            var cfg = CreateCommandConfig(TimeSpan.FromSeconds(10), "10.10.10.70");

            Assert.AreEqual("-v -i 10.10.10.70", cfg.Arguments);
        }

        [Test]
        [MaxTime(10000)]
        public async Task WindowsFtpCommand_ExecuteAsync()
        {
            this.SkipTestOnLinux();

            IExitInfo result = null;

            // command will exit before kill is issued
            var cfg = CreateCommandConfig(TimeSpan.FromSeconds(30));

            using (var child = new WindowsFtpCommand(cfg, this.Logger))
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
        public async Task WindowsFtpCommand_UnknownHost()
        {
            this.SkipTestOnLinux();

            IExitInfo result = null;

            // command will exit before kill is issued
            var cfg = CreateCommandConfig(TimeSpan.FromSeconds(30), "asdfasdf2345sdfg");
            cfg.LoginAsAnonymous = true;

            using (var child = new WindowsFtpCommand(cfg, this.Logger))
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

            Assert.That(result.ErrorOutput, Has.Count.EqualTo(0));
            Assert.That(result.StandardOutput, Has.Count.EqualTo(1));

            Assert.AreEqual("Unknown host asdfasdf2345sdfg.", result.StandardOutput.First().Message);

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
        [MaxTime(15000)]
        public async Task WindowsFtpCommand_UbuntuFtpSite()
        {
            this.SkipTestOnLinux();

            IExitInfo result = null;

            // command will exit before kill is issued
            var cfg = CreateCommandConfig(TimeSpan.FromSeconds(10), "ftp.ubuntu.com");
            cfg.LoginAsAnonymous = true;

            using (var child = new WindowsFtpCommand(cfg, this.Logger))
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

            Assert.That(result.ErrorOutput, Has.Count.EqualTo(1));
            Assert.That(result.StandardOutput, Has.Count.EqualTo(6));

            Assert.That(result.ErrorOutput.First().Message.StartsWith("Anonymous login succeeded for", StringComparison.InvariantCultureIgnoreCase));
            Assert.AreEqual("Connected to ftp.ubuntu.com.", result.StandardOutput.OrderBy(x => x.Time).First().Message);
            Assert.AreEqual("221 Goodbye.", result.StandardOutput.OrderBy(x => x.Time).Last().Message);

            try
            {
                System.Threading.Thread.Sleep(1000);

                using var p = Process.GetProcessById(result.StartInfo.ProcessId.Value);

                Assert.IsNull(p);
            }
            catch (Exception)
            {
                return;
            }

            Assert.Fail("Child process should have exited");
        }

        private static WindowsFtpOptions CreateCommandConfig(TimeSpan timeout, string host = null)
        {
            if (host == null)
            {
                host = Environment.GetEnvironmentVariable("UNITTEST_FTP_HOST");
            }

            var user = Environment.GetEnvironmentVariable("UNITTEST_FTP_USERNAME");
            var pass = Environment.GetEnvironmentVariable("UNITTEST_FTP_PASSWORD");

            var cfg = new WindowsFtpOptions(host)
            {
                DisableInteractiveMode = true,
                EnableDebugging = false,
                SuppressRemoteServerResponses = true,
                CommandTimeout = timeout,
                Credential = new NetworkCredential(user, pass),
            };

            return cfg;
        }
    }
}