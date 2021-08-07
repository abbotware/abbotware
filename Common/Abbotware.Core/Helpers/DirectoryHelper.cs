// -----------------------------------------------------------------------
// <copyright file="DirectoryHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    /// <summary>
    ///     Directory Helper functions
    /// </summary>
    public static class DirectoryHelper
    {
        private static readonly object Mutex = new();

        private static bool workingDirectoryInitialized;

        private static Assembly DefaultAnchor => Assembly.GetEntryAssembly();

        /// <summary>
        ///     Sets the working directory to the directory containing the executing assembly
        /// </summary>
        public static void SetWorkingDirectoryToExecutingAssembly() => SetWorkingDirectory(Assembly.GetExecutingAssembly());

        /// <summary>
        /// Sets the working directory to folder of the anchor type's assembly
        /// </summary>
        /// <typeparam name="TAnchorType">type to use for anchor assembly</typeparam>
        public static void SetWorkingDirectory<TAnchorType>() => SetWorkingDirectory(typeof(TAnchorType).Assembly);

        /// <summary>
        /// Sets the working directory to folder of the provided anchor assembly
        /// </summary>
        /// <param name="anchor">assembly to use as an anchor reference</param>
        public static void SetWorkingDirectory(Assembly anchor)
        {
            anchor = Arguments.EnsureNotNull(anchor, nameof(anchor));

            SetWorkingDirectory(new DirectoryInfo(Path.GetDirectoryName(anchor.CodeBase)));
        }

        /// <summary>
        /// Sets the working directory to the provided Uri
        /// </summary>
        /// <param name="directory">directory to use</param>
        public static void SetWorkingDirectory(DirectoryInfo directory)
        {
            directory = Arguments.EnsureNotNull(directory, nameof(directory));

            lock (Mutex)
            {
                if (workingDirectoryInitialized)
                {
                    throw new InvalidOperationException($"SetWorkingDirectory already called - Working Directory is currently: {Environment.CurrentDirectory}, attempted to set: {directory.FullName}");
                }

                if (!directory.Exists)
                {
                    throw new DirectoryNotFoundException($"SetWorkingDirectory:{directory.FullName}");
                }

                Environment.CurrentDirectory = directory.FullName;

                workingDirectoryInitialized = true;
            }
        }

        /// <summary>
        ///     Generates full paths for searching for a file
        /// </summary>
        /// <param name="fileName">filename to search</param>
        /// <returns>list of search paths</returns>
        public static IEnumerable<string> GenerateSearchPaths(string fileName)
        {
            return GenerateSearchPaths(fileName, string.Empty);
        }

        /// <summary>
        ///     Generates full paths for searching for a file
        /// </summary>
        /// <param name="fileName">filename to search</param>
        /// <param name="folderName">subfolder</param>
        /// <returns>list of search paths</returns>
        public static IEnumerable<string> GenerateSearchPaths(string fileName, string folderName)
        {
            return GenerateSearchPaths(fileName, folderName, DefaultAnchor);
        }

        /// <summary>
        ///     Generates full paths for searching for a file
        /// </summary>
        /// <param name="fileName">filename to search</param>
        /// <param name="folderName">subfolder</param>
        /// <param name="anchor">anchor assembly to use for relative pathing</param>
        /// <returns>list of search paths</returns>
        public static IEnumerable<string> GenerateSearchPaths(string fileName, string folderName, Assembly anchor)
        {
            var paths = new List<string>();

            if (!string.IsNullOrWhiteSpace(folderName))
            {
                paths.AddRange(GenerateRecursivePaths(new PathGenerationInfo { File = fileName, Folder = folderName }));

                if (anchor != null)
                {
                    var assemmblyLocation = anchor.Location;
                    var directory = Path.GetDirectoryName(assemmblyLocation);

                    paths.AddRange(GenerateRecursivePaths(new PathGenerationInfo { Root = directory, File = fileName, Folder = folderName }));
                }
            }

            paths.AddRange(GenerateRecursivePaths(new PathGenerationInfo { File = fileName }));

            return paths;
        }

        /// <summary>
        ///     Best effort for locating a file
        /// </summary>
        /// <param name="file">file to locate</param>
        /// <returns>file path</returns>
        public static string FindFilePath(string file)
        {
            return FindFilePath(file, string.Empty);
        }

        /// <summary>
        ///     Best effort for locating a file
        /// </summary>
        /// <param name="file">file to locate</param>
        /// <param name="folder">folder name</param>
        /// <returns>file path</returns>
        public static string FindFilePath(string file, string folder)
        {
            return FindFilePath(file, folder, DefaultAnchor);
        }

        /// <summary>
        ///     Best effort for locating a file
        /// </summary>
        /// <param name="file">file to locate</param>
        /// <param name="folder">folder name</param>
        /// <param name="anchor">anchor assembly to use for relative pathing</param>
        /// <returns>file path</returns>
        public static string FindFilePath(string file, string folder, Assembly anchor)
        {
            var paths = GenerateSearchPaths(file, folder, anchor);

            foreach (var path in paths)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }

            throw new FileNotFoundException(file);
        }

        /// <summary>
        ///     Best effort for locating a directory
        /// </summary>
        /// <param name="directory">directory to locate</param>
        /// <param name="folder">folder name</param>
        /// <returns>directory path</returns>
        public static string FindDirectoryPath(string directory, string folder)
        {
            return FindDirectoryPath(directory, folder, DefaultAnchor);
        }

        /// <summary>
        ///     Best effort for locating a directory
        /// </summary>
        /// <param name="directory">directory to locate</param>
        /// <param name="folder">folder name</param>
        /// <param name="anchor">anchor assembly to use for relative pathing</param>
        /// <returns>directory path</returns>
        public static string FindDirectoryPath(string directory, string folder, Assembly anchor)
        {
            var paths = GenerateSearchPaths(directory, folder, anchor);

            foreach (var path in paths)
            {
                if (Directory.Exists(path))
                {
                    return path;
                }
            }

            throw new DirectoryNotFoundException(directory);
        }

        private static List<string> GenerateRecursivePaths(PathGenerationInfo pathInfo)
        {
            var paths = new List<string>();

            if (string.IsNullOrWhiteSpace(pathInfo.Root))
            {
                pathInfo.Root = Environment.CurrentDirectory;
            }

            if (string.IsNullOrWhiteSpace(pathInfo.Folder))
            {
                pathInfo.Folder = string.Empty;
            }

            var folder = Path.Combine(pathInfo.Root, pathInfo.Folder, pathInfo.File);
            paths.Add(folder);

            folder = Path.Combine(pathInfo.Root, "..", pathInfo.Folder, pathInfo.File);
            paths.Add(folder);

            return paths;
        }

        /// <summary>
        ///     Info class for path generation
        /// </summary>
        internal class PathGenerationInfo
        {
            /// <summary>
            ///     Gets or sets the root path
            /// </summary>
            public string Root { get; set; } = string.Empty;

            /// <summary>
            ///     Gets or sets the file name
            /// </summary>
            public string File { get; set; } = string.Empty;

            /// <summary>
            ///     Gets or sets the folder name
            /// </summary>
            public string Folder { get; set; } = string.Empty;
        }
    }
}