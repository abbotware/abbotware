// -----------------------------------------------------------------------
// <copyright file="IBidirectionalConverter{TFrom,TTo}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
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
        /// Gets the From -> To Converter
        /// </summary>
        IConverter<TFrom, TTo> Forward { get; }

        /// <summary>
        /// Gets the To -> From Converter
        /// </summary>
        IConverter<TTo, TFrom> Reverse { get; }
    }
}