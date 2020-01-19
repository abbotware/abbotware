// -----------------------------------------------------------------------
// <copyright file="IPoint{TX,TY}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Math
{
    /// <summary>
    ///     interface for a point with 2 dimensions
    /// </summary>
    /// <typeparam name="TX">X dimension data type</typeparam>
    /// <typeparam name="TY">Y dimension data type</typeparam>
    public interface IPoint<out TX, out TY>
    {
        /// <summary>
        ///     Gets the X value
        /// </summary>
        TX X { get; }

        /// <summary>
        ///     Gets the Y value
        /// </summary>
        TY Y { get; }
    }
}