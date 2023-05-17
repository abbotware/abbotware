// -----------------------------------------------------------------------
// <copyright file="ICurve{TX,TY}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Math
{
    /// <summary>
    /// interface for a curve
    /// </summary>
    /// <typeparam name="TX">X dimension data type</typeparam>
    /// <typeparam name="TY">Y dimension data type</typeparam>
    public interface ICurve<TX, TY>
        where TX : notnull
        where TY : notnull
    {
        /// <summary>
        /// Gets the y value for a given x value
        /// </summary>
        /// <param name="x">x value</param>
        /// <returns>y value</returns>
        TY GetPoint(TX x);
    }
}