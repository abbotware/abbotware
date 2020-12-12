// -----------------------------------------------------------------------
// <copyright file="ParserContext.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
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
        /// Gets or sets the file path
        /// </summary>
        public Uri Path { get; set; }
    }
}