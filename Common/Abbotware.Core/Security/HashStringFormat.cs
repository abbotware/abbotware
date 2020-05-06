// -----------------------------------------------------------------------
// <copyright file="HashStringFormat.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Security
{
    /// <summary>
    /// output format of the hash value string
    /// </summary>
    public enum HashStringFormat
    {
        /// <summary>
        /// Encoded in Hex  / Base 16 (default)
        /// </summary>
        Hex = 0,

        /// <summary>
        /// Encoded in Base 64
        /// </summary>
        Base64 = 1,
    }
}