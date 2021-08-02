// -----------------------------------------------------------------------
// <copyright file="RuntimeCallerInformation.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Diagnostics
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// class to capture the runtime caller attributes
    /// </summary>
    public class RuntimeCallerInformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeCallerInformation"/> class.
        /// </summary>
        /// <param name="memberName">compiler injected caller member name</param>
        /// <param name="sourceFilePath">compiler injected source file path</param>
        /// <param name="sourceLineNumber">compiler injected source line number</param>
        public RuntimeCallerInformation([CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int? sourceLineNumber = 0)
        {
            this.MemberName = memberName;
            this.SourceFilePath = sourceFilePath;
            this.SourceLineNumber = sourceLineNumber;
        }

        /// <summary>
        /// Gets the Class member name
        /// </summary>
        public string MemberName { get; }

        /// <summary>
        /// Gets the Class name
        /// </summary>
        [Obsolete("reserved for future use")]
        public string ClassName { get; } = string.Empty;

        /// <summary>
        /// Gets the Source code file path
        /// </summary>
        public string SourceFilePath { get; }

        /// <summary>
        /// Gets the Source code file name
        /// </summary>
        public string SourceFileName => Path.GetFileName(this.SourceFilePath);

        /// <summary>
        /// Gets the Source code line Number
        /// </summary>
        public int? SourceLineNumber { get; }
    }
}