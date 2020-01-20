// -----------------------------------------------------------------------
// <copyright file="WindowsOperatingSystem.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows
{
    using System;
    using System.Diagnostics;
    using Abbotware.Core.Exceptions;
    using Abbotware.Core.Runtime;
    using Abbotware.Interop.Windows.Kernel32;
    using Abbotware.Interop.Windows.User32;

    /// <summary>
    ///     Windows Operating System Specific functionality
    /// </summary>
    public class WindowsOperatingSystem : BaseEnvironment, IOperatingSystem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsOperatingSystem"/> class.
        /// </summary>
        public WindowsOperatingSystem()
        {
            Kernel32Methods.SetConsoleCtrlHandler(this.OnConsoleCtrlEvent, true);
        }

        /// <inheritdoc />
        public override long SystemMemory
        {
            get
            {
                var memStatus = Kernel32Methods.GlobalMemoryStatusEx();

                return (long)memStatus.TotalPhysical;
            }
        }

        /// <inheritdoc />
        public TimeSpan? SystemUptime
        {
            get
            {
                if (!Stopwatch.IsHighResolution)
                {
                    throw new InvalidOperationException();
                }

                var ticks = Stopwatch.GetTimestamp();
                var uptime = (double)ticks / Stopwatch.Frequency;
                return TimeSpan.FromSeconds(uptime);
            }
        }

        /// <inheritdoc />
        public void Reboot()
        {
            User32Methods.ExitWindowsEx(ShutdownMethods.Reboot, ForceShutdownMethods.ForceIfHung, ShutdownReasons.MajorApplication | ShutdownReasons.MinorOther);
        }

        /// <summary>
        ///     callback for Kernel32's SetConsoleCtrlHandler
        /// </summary>
        /// <param name="ctrlType">the control event received</param>
        /// <returns>true if the callback handled the event</returns>
        private bool OnConsoleCtrlEvent(ConsoleCtrlType ctrlType)
        {
            switch (ctrlType)
            {
                case ConsoleCtrlType.CtrlCEvent:
                case ConsoleCtrlType.CtrlBreakEvent:
                case ConsoleCtrlType.CloseEvent:
                case ConsoleCtrlType.LogOffEvent:
                case ConsoleCtrlType.ShutdownEvent:
                    {
                        this.ShutdownSignal.SetResult(true);
                        return true;
                    }

                default:
                    {
                        throw AbbotwareException.Create("Unexpected Shutdown Event Recieved: {0} Should not reach this code", ctrlType);
                    }
            }
        }
    }
}