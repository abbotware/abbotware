// -----------------------------------------------------------------------
// <copyright file="ParserContext.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.Plugins.Configuration
{
    using System;

    /// <summary>
    /// Context for the parser
    /// </summary>
    public class ParserContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParserContext"/> class.
        /// </summary>
        /// <param name="uri">file path uri</param>
        public ParserContext(Uri uri) => this.Path = uri;

        /// <summary>
        /// Gets the file path uri
        /// </summary>
        public Uri Path { get; }
    }
}