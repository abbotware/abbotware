// -----------------------------------------------------------------------
// <copyright file="CopyType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core
{
    /// <summary>
    ///     gets the copy type
    /// </summary>
    public enum CopyType
    {
        /// <summary>
        ///     Create a deep copy
        /// </summary>
        Deep,

        /// <summary>
        ///     Create a shallow copy
        /// </summary>
        Shallow,
    }
}