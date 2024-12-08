// <copyright file="Advapi32Tests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Abbotware.ManualTests.Interop.Windows
{
    using Abbotware.Interop.NUnit;
    using Abbotware.Interop.Windows.Advapi32;
    using NUnit.Framework;

    /// <summary>
    ///     Advapi32 unit tests
    /// </summary>
    [TestFixture]
    [Category("ManualTests")]
    [Category("Interop.Yubico")]
    [Category("Interop.Yubico.ManualTests")]
    public class Advapi32Tests
    {
        [Test]
        [ExpectedException(typeof(System.ComponentModel.Win32Exception))]
        public void Advapi32_LogonUser_BadPassword()
        {
            Advapi32Methods.LogonUser("unittest", "abbotware", "ddddd", LogOnType.Network, LogOnProviderType.Default);
        }

        [Test]
        public void Advapi32_LogonUser_GoodPassword()
        {
            var token = Advapi32Methods.LogonUser("unittest", "abbotware", "some password", LogOnType.Network, LogOnProviderType.Default);
            Assert.That(token, Is.Not.Null);
            Assert.That(token.IsInvalid, Is.False);
        }
    }
}