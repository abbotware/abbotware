// <copyright file="Kernel32TestsKernel32Tests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>

namespace Abbotware.IntegrationTests.Interop.Windows
{
    using Abbotware.Interop.Windows.Kernel32;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using global::NUnit.Framework;

    /// <summary>
    ///     Kernel32 UnitTests
    /// </summary>
    [TestFixture]
    [Category("Interop.Win32")]
    [Category("windows")]
    public class Kernel32Tests : BaseNUnitTest
    {
        [Test]
        public void Kernel32_SetDllDirectory()
        {
            this.SkipTestOnLinux();

            Kernel32Methods.ResetDllDirectory();

            Kernel32Methods.SetDllDirectory("test");

            Kernel32Methods.ResetDllDirectory();
        }
    }
}