// -----------------------------------------------------------------------
// <copyright file="DirectoryInfoExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Extensions;

using System;
using System.Collections.Generic;
using System.IO;
using Abbotware.Core.IO;

/// <summary>
///     DirectoryInfo Extension Methods
/// </summary>
public static partial class DirectoryInfoExtensions
{
    /// <summary>
    /// Ensures that the specified <see cref="DirectoryInfo"/> exists on disk.
    /// If it does not exist, the directory is created.
    /// </summary>
    /// <param name="extended">The <see cref="DirectoryInfo"/> instance to check or create.</param>
    public static void EnsureCreated(this DirectoryInfo extended)
    {
        // Check if the directory exists
        if (!extended.Exists)
        {
            // Create the directory (and any missing parent directories)
            extended.Create();
        }
    }

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
    /// <returns>FileInfo object</returns>
    /// <exception cref="FileNotFoundException">if file does not exist</exception>
    public static FileInfo FileInfoForExisting(this DirectoryInfo extend, params IList<string> parts)
        => extend.FileInfo(FileExistenceBehavior.ThrowIfNotExists, parts);

    /// <summary>
    /// Gets FileInfo for a new file in the directory
    /// </summary>
    /// <param name="extend">extended directory info</param>
    /// <param name="parts">path parts names</param>
    /// <returns>FileInfo object</returns>
    public static FileInfo FileInfo(this DirectoryInfo extend, params IList<string> parts)
        => extend.FileInfo(FileExistenceBehavior.NotSpecified, parts);

    /// <summary>
    /// Gets FileInfo for a new file in the directory
    /// </summary>
    /// <param name="extend">extended directory info</param>
    /// <param name="parts">path parts names</param>
    /// <returns>FileInfo object</returns>
    public static FileInfo FileInfoForNew(this DirectoryInfo extend, params IList<string> parts)
        => extend.FileInfo(FileExistenceBehavior.ThrowIfExists, parts);

    /// <summary>
    /// Gets FileInfo for a file in the directory (if it exists)
    /// </summary>
    /// <param name="extend">extended directory info</param>
    /// <param name="behavior">behavior for file existence</param>
    /// <param name="parts">path parts names</param>
    /// <returns>FileInfo object</returns>
    /// <exception cref="FileNotFoundException">if file does not exist</exception>
    private static FileInfo FileInfo(this DirectoryInfo extend, FileExistenceBehavior behavior, params IList<string> parts)
    {
        parts.Insert(0, extend.FullName);
        var fi = new FileInfo(Path.Combine([.. parts]));

        switch (behavior)
        {
            case FileExistenceBehavior.ThrowIfExists:
                if (fi.Exists)
                {
                    throw new InvalidOperationException($"{fi.FullName} file already exists");
                }

                break;

            case FileExistenceBehavior.ThrowIfNotExists:
                if (!fi.Exists)
                {
                    throw new FileNotFoundException($"{fi.FullName} not found", fi.FullName);
                }

                break;
            case FileExistenceBehavior.NotSpecified:
            default:
                break;
        }

        return fi;
    }
}