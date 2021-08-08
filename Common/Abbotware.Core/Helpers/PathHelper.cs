// -----------------------------------------------------------------------
// <copyright file="PathHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;

    /// <summary>
    ///     Helper methods related to paths
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        ///     interal regex to clean directory names
        /// </summary>
        private static readonly Regex CleanDirectoryRegex = InitDirectoryRegex();

        /// <summary>
        ///     interal regex to clean file names
        /// </summary>
        private static readonly Regex CleanFileRegex = InitFileRegex();

        /// <summary>
        ///     Helper method to clean up directory name
        /// </summary>
        /// <param name="name">name to clean</param>
        /// <returns>cleaned name</returns>
        public static string CleanDirectory(string name)
        {
            name = Arguments.EnsureNotNullOrWhitespace(name, nameof(name));

            var output = CleanDirectoryRegex.Replace(name, string.Empty);

            return output;
        }

        /// <summary>
        ///     Helper method to clean up file name
        /// </summary>
        /// <param name="name">name to clean</param>
        /// <returns>cleaned name</returns>
        public static string CleanFile(string name)
        {
            name = Arguments.EnsureNotNullOrWhitespace(name, nameof(name));

            var output = CleanFileRegex.Replace(name, string.Empty);

            return output;
        }

        /// <summary>
        ///     Helper method to clean up file name
        /// </summary>
        /// <param name="path">name to clean</param>
        /// <returns>cleaned name</returns>
        public static string CleanPath(string path)
        {
            path = Arguments.EnsureNotNullOrWhitespace(path, nameof(path));

            var pathWithoutRoot = path;
            var root = string.Empty;

            if (Path.IsPathRooted(path))
            {
                root = Path.GetPathRoot(path)!;
#if NETSTANDARD2_0
                pathWithoutRoot = path.Substring(root.Length);
#else
                pathWithoutRoot = path[root.Length..];
#endif
            }

            var dir = Path.GetDirectoryName(pathWithoutRoot)!;
            var file = Path.GetFileName(pathWithoutRoot);

            var newDir = CleanDirectory(dir);
            var newFile = CleanFile(file);

            var cleanPath = Path.Combine(root, newDir, newFile);

            return cleanPath;
        }

        /// <summary>
        /// search for file
        /// </summary>
        /// <param name="filePaths">list of files</param>
        /// <param name="notFoundMessage">not found message to throw</param>
        /// <returns>first found find</returns>
        public static string FindFirstFile(IEnumerable<string> filePaths, string notFoundMessage)
        {
            var file = FindFirstFileOrNone(filePaths);

            if (file == null)
            {
                throw new FileNotFoundException(notFoundMessage);
            }

            return file;
        }

        /// <summary>
        /// search for file
        /// </summary>
        /// <param name="filePaths">list of files</param>
        /// <returns>first found find</returns>
        public static string? FindFirstFileOrNone(IEnumerable<string> filePaths)
        {
            filePaths = Arguments.EnsureNotNull(filePaths, nameof(filePaths));

            foreach (var p in filePaths)
            {
                if (File.Exists(p))
                {
                    return p;
                }
            }

            return null;
        }

        /// <summary>
        ///     Init logic for directory regex
        /// </summary>
        /// <returns>directory regex</returns>
        private static Regex InitDirectoryRegex()
        {
            var dirRegexSearch = new string(Path.GetInvalidPathChars()) + ':';
            var dpattern = FormattableString.Invariant($"[{Regex.Escape(dirRegexSearch)}]");
            return new Regex(dpattern);
        }

        /// <summary>
        ///     Init logic for file regex
        /// </summary>
        /// <returns>file regex</returns>
        private static Regex InitFileRegex()
        {
            var fileRegexSearch = new string(Path.GetInvalidFileNameChars()) + ':';
            var fpattern = FormattableString.Invariant($"[{Regex.Escape(fileRegexSearch)}]");
            return new Regex(fpattern);
        }
    }
}