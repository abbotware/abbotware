// -----------------------------------------------------------------------
// <copyright file="ParserConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.Plugins.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Abbotware.Core.Configuration;

    /// <summary>
    ///     configuration class for parsing
    /// </summary>
    public class ParserConfiguration : BaseOptions
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParserConfiguration" /> class.
        /// </summary>
        public ParserConfiguration()
        {
            this.HasHeaders = true;
            this.DelimiterChar = ',';
            this.QuoteChar = '"';
            this.EscapeChar = '"';
            this.CommentChar = '#';
            this.BufferSize = 4096;

            this.LogOptions = false;
        }

        ////[Obsolete("future use")]
        ////public string Preprocess { get; set; }

        ////[Obsolete("future use")]
        ////public NetworkCredential Credential { get; set; }

        ////[Obsolete("future use")]
        ////public string FileNamePattern { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the file has headers
        /// </summary>
        public bool HasHeaders { get; set; }

        /// <summary>
        ///     Gets or sets the delimiter char
        /// </summary>
        public char DelimiterChar { get; set; }

        /// <summary>
        ///     Gets or sets the quote char
        /// </summary>
        public char QuoteChar { get; set; }

        /// <summary>
        ///     Gets or sets the escape char
        /// </summary>
        public char EscapeChar { get; set; }

        /// <summary>
        ///     Gets or sets the comment char
        /// </summary>
        public char CommentChar { get; set; }

        /// <summary>
        ///     Gets or sets the buffer size
        /// </summary>
        public int BufferSize { get; set; }

        /// <summary>
        ///     Gets or sets the number of lines to skip before the header
        /// </summary>
        public int SkipLinesBeforeHeader { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether its ok for the file to have extra fields
        /// </summary>
        public bool AllowFileToHaveExtraProperties { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether its ok for the class to have extra properties
        /// </summary>
        public bool AllowClassToHaveExtraProperties { get; set; }

        /// <summary>
        ///     Gets the custom property conversion functors
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "reviewed")]
        public Dictionary<string, Func<string, object>> CustomPropertyConvertors { get; } = new Dictionary<string, Func<string, object>>(StringComparer.OrdinalIgnoreCase);
    }
}