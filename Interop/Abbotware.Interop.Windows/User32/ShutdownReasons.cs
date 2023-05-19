// -----------------------------------------------------------------------
// <copyright file="ShutdownReasons.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows.User32
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     Defines the reason for the shutdown of the system.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "Following Win32 API Spec")]
    [SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Following Win32 API Spec")]
    [Flags]
    public enum ShutdownReasons : uint
    {
        /// <summary>
        ///     From MSDN
        /// </summary>
        MajorApplication = 0x00040000,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MajorHardware = 0x00010000,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MajorLegacyApi = 0x00070000,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MajorOperatingSystem = 0x00020000,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MajorPower = 0x00060000,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MajorSoftware = 0x00030000,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MajorSystem = 0x00050000,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorBlueScreen = 0x0000000F,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorCordUnplugged = 0x0000000b,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorDisk = 0x00000007,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorEnvironment = 0x0000000c,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorHardwareDriver = 0x0000000d,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorHotfix = 0x00000011,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorHung = 0x00000005,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorInstallation = 0x00000002,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorMaintenance = 0x00000001,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorMMC = 0x00000019,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorNetworkConnectivity = 0x00000014,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorNetworkCard = 0x00000009,

        /// <summary>
        ///     From MSDN
        /// </summary>
        Other = 0x00000000,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorOtherDriver = 0x0000000e,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorPowerSupply = 0x0000000a,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorProcessor = 0x00000008,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorReconfig = 0x00000004,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorSecurity = 0x00000013,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorSecurityFix = 0x00000012,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorSecurityFixUninstall = 0x00000018,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorServicePack = 0x00000010,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorServicePackUninstall = 0x00000016,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorTermSrv = 0x00000020,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorUnstable = 0x00000006,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorUpgrade = 0x00000003,

        /// <summary>
        ///     From MSDN
        /// </summary>
        MinorWMI = 0x00000015,

        /// <summary>
        ///     From MSDN
        /// </summary>
        FlagUserDefined = 0x40000000,

        /// <summary>
        ///     From MSDN
        /// </summary>
        FlagPlanned = 0x80000000,
    }
}