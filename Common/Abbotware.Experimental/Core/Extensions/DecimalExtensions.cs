// -----------------------------------------------------------------------
// <copyright file="DecimalExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions
{
    using System;

    /// <summary>
    ///     Decimal extensions
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        ///     Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="extended">decimal object being extended</param>
        /// <param name="power">power to raise</param>
        /// <returns>
        ///     The number<paramref name="extended" />  raised to the power <paramref name="power" />
        /// </returns>
        public static decimal Pow(this decimal extended, double power)
        {
            return (decimal)Math.Pow((double)extended, power);
        }
    }
}