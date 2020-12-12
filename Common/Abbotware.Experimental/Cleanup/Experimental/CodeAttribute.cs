// -----------------------------------------------------------------------
// <copyright file="CodeAttribute.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     Attribute that contains metadata about the code quality of the class
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    [ExcludeFromCodeCoverage]
    public sealed class CodeAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CodeAttribute" /> class.
        /// </summary>
        /// <param name="quality">code quality</param>
        public CodeAttribute(Quality quality)
            : this(quality, string.Empty)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CodeAttribute" /> class.
        /// </summary>
        /// <param name="quality">code quality</param>
        /// <param name="message">code message</param>
        public CodeAttribute(Quality quality, string message)
        {
            this.Quality = quality;
            this.Message = message;
        }

        /// <summary>
        ///     Gets the code quality
        /// </summary>
        public Quality Quality { get; }

        /// <summary>
        ///     Gets the code quality
        /// </summary>
        public string Message { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether or not Code Analysis is complete for this class
        /// </summary>
        public bool CodeAnalysisComplete { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether or not Code Contracts are complete for this class
        /// </summary>
        public bool CodeContractsComplete { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether or not Code Coverage is complete for this class
        /// </summary>
        public bool CodeCoverageComplete { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether or not Style Cope is complete for this class
        /// </summary>
        public bool StyleCopComplete { get; set; }
    }
}