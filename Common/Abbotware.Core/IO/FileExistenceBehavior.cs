// -----------------------------------------------------------------------
// <copyright file="FileExistenceBehavior.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.IO;

/// <summary>
/// Behavior for File Existence
/// </summary>
public enum FileExistenceBehavior
{
    /// <summary>
    /// Do nothing
    /// </summary>
    NotSpecified,

    /// <summary>
    /// Throw only if file exists
    /// </summary>
    ThrowIfExists,

    /// <summary>
    /// Throw only if file does not exist
    /// </summary>
    ThrowIfNotExists,
}
