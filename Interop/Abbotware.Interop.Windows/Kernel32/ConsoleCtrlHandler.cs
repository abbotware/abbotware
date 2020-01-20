// -----------------------------------------------------------------------
// <copyright file="ConsoleCtrlHandler.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows.Kernel32
{
    /// <summary>
    ///     A delegate type to be used as the handler routine for SetConsoleCtrlHandler.
    /// </summary>
    /// <param name="ctrlType">The CtrlType parameter identifies which control signal was received, </param>
    /// <returns>indicates whether the signal was handled.</returns>
    public delegate bool ConsoleCtrlHandler(ConsoleCtrlType ctrlType);
}