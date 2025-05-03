// -----------------------------------------------------------------------
// <copyright file="DirectoryInfoExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Extensions;

using System.Collections.Generic;
using System.IO;

/// <summary>
///     DirectoryInfo Extension Methods
/// </summary>
public static class DirectoryInfoExtensions
{
    /// <summary>
    /// Gets a DirectoryInfo for a specified sub directory (if it exists)
    /// </summary>
    /// <param name="extend">extended directory info</param>
    /// <param name="folders">folder names</param>
    /// <returns>DirectoryInfo object</returns>
    /// <exception cref="DirectoryNotFoundException">if sub directory does not exist</exception>
    public static DirectoryInfo SubDirectoryInfo(this DirectoryInfo extend, params IList<string> folders)
    {
        folders.Insert(0, extend.FullName);
        var di = new DirectoryInfo(Path.Combine([.. folders]));

        if (!di.Exists)
        {
            throw new DirectoryNotFoundException($"{di.FullName} not found");
        }

        return di;
    }

    /// <summary>
    /// Gets a FileInfo for a specified sub file (if it exists)
    /// </summary>
    /// <param name="extend">extended directory info</param>
    /// <param name="parts">path parts names</param>
    /// <returns>DirectoryInfo object</returns>
    /// <exception cref="DirectoryNotFoundException">if sub directory does not exist</exception>
    public static FileInfo FileInfo(this DirectoryInfo extend, params IList<string> parts)
        => extend.FileInfo(true, parts);

    /// <summary>
    /// Gets a FileInfo for a specified sub file (if it exists)
    /// </summary>
    /// <param name="extend">extended directory info</param>
    /// <param name="throwIfNotExist">throw if file doesnt exist</param>
    /// <param name="parts">path parts names</param>
    /// <returns>DirectoryInfo object</returns>
    /// <exception cref="DirectoryNotFoundException">if sub directory does not exist</exception>
    public static FileInfo FileInfo(this DirectoryInfo extend, bool throwIfNotExist, params IList<string> parts)
    {
        parts.Insert(0, extend.FullName);
        var fi = new FileInfo(Path.Combine([.. parts]));

        if (throwIfNotExist & !fi.Exists)
        {
            throw new FileNotFoundException($"{fi.FullName} not found");
        }

        return fi;
    }
}