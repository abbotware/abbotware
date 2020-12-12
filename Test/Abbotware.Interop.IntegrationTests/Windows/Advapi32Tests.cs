// <copyright file="Advapi32Tests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Abbotware.IntegrationTests.Interop.Windows
{
    using Abbotware.Interop.NUnit;
    using Abbotware.Interop.Windows.Advapi32;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using global::NUnit.Framework;

    /// <summary>
    ///     Advapi32 unit tests
    /// </summary>
    [TestFixture]
    [Category("Interop.Win32")]
    public class Advapi32Tests : BaseNUnitTest
    {
        [Test]
        [ExpectedException(typeof(System.ComponentModel.Win32Exception))]
        [Category("windows")]
        public void Advapi32_LogonUser_BadPassword()
        {
            this.SkipTestOnLinux();

            var token = Advapi32Methods.LogonUser("unittest", "abbotware", "ddddd", LogOnType.Network, LogOnProviderType.Default);

            Assert.NotNull(token);
        }
    }
}