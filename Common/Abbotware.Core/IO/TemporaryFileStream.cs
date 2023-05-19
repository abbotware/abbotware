// -----------------------------------------------------------------------
// <copyright file="TemporaryFileStream.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.IO
{
    using System;
    using System.IO;

    /// <summary>
    ///     Temp file that gets deleted on closing
    /// </summary>
    public class TemporaryFileStream : FileStream
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TemporaryFileStream" /> class.
        /// </summary>
        public TemporaryFileStream()
            : this(new Uri(Path.GetTempFileName()))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TemporaryFileStream" /> class.
        /// </summary>
        /// <param name="filePath">path to use for creating the temp file</param>
        public TemporaryFileStream(Uri filePath)
            : base(
                  Arguments.EnsureNotNull(filePath, nameof(filePath)).LocalPath,
                  FileMode.OpenOrCreate,
                  FileAccess.ReadWrite,
                  FileShare.Read,
                  1024,
                  FileOptions.DeleteOnClose)
        {
        }
    }
}
