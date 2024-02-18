// -----------------------------------------------------------------------
// <copyright file="UIntExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

#if NET7_0_OR_GREATER
namespace Abbotware.Core.Extensions
{
    using System;
    using System.Globalization;
    using System.Numerics;

    /// <summary>
    /// UInt Extension methods
    /// </summary>
    public static class UIntExtensions
    {
        /// <summary>
        /// Converts to generic number
        /// </summary>
        /// <typeparam name="T">Number Type</typeparam>
        /// <param name="number">number to convert</param>
        /// <returns>converted number</returns>
        public static T ToGenericNumber<T>(this uint number)
            where T : INumber<T>
        {
            return T.One * (T)Convert.ChangeType(number, typeof(T), CultureInfo.InvariantCulture)!;
        }
    }
}
#endif