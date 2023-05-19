// -----------------------------------------------------------------------
// <copyright file="Quality.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core
{
    /// <summary>
    ///     Describes the quality of the class
    /// </summary>
    public enum Quality
    {
        /// <summary>
        ///     This is experimental and not recommended for use.  Might be removed in the future
        /// </summary>
        Experimental,

        /// <summary>
        ///     This generally works and will be finalized
        /// </summary>
        InDevelopment,

        /// <summary>
        ///     This requires complete refactoring / replacing with a different library
        /// </summary>
        RefactorOrReplace,

        /// <summary>
        ///     This generally works and will be finalized
        /// </summary>
        RequiresUpdating,

        /// <summary>
        ///     Complete
        /// </summary>
        Production,
    }
}