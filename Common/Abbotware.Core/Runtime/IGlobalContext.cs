// -----------------------------------------------------------------------
// <copyright file="IGlobalContext.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Runtime;

/// <summary>
/// Global Runtime context with acess to various information
/// </summary>
public interface IGlobalContext
{
    /// <summary>
    /// Gets the evironment context
    /// </summary>
    public IEnvironmentInformation Environment { get; }

    /// <summary>
    /// Gets the application context
    /// </summary>
    public IApplicationInformation Application { get; }

    /// <summary>
    /// Gets the operating system information
    /// </summary>
    public IOperatingSystemInformation OperatingSystem { get; }

    /// <summary>
    /// Gets the process information
    /// </summary>
    public IProcessInformation Process { get; }
}
