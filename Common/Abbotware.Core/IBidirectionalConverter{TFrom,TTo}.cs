// -----------------------------------------------------------------------
// <copyright file="IBidirectionalConverter{TFrom,TTo}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core
{
    /// <summary>
    /// Interface for bi directional converting
    /// </summary>
    /// <typeparam name="TFrom">From Type</typeparam>
    /// <typeparam name="TTo">To Typ</typeparam>
    public interface IBidirectionalConverter<TFrom, TTo>
    {
        /// <summary>
        /// Converts TFrom to TTo
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>converted</returns>
        TTo Convert(TFrom input);

        /// <summary>
        /// Converts TTo to TFrom
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>converted</returns>
        TFrom Convert(TTo input);
    }
}