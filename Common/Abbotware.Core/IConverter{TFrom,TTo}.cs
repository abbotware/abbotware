// -----------------------------------------------------------------------
// <copyright file="IConverter{TFrom,TTo}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core
{
    /// <summary>
    /// Interface for converting
    /// </summary>
    /// <typeparam name="TFrom">From Type</typeparam>
    /// <typeparam name="TTo">To Typ</typeparam>
    public interface IConverter<TFrom, TTo>
    {
        /// <summary>
        /// Converts From to To
        /// </summary>
        /// <param name="from">from value</param>
        /// <returns>to value</returns>
        TTo Convert(TFrom from);
    }
}