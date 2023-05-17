// -----------------------------------------------------------------------
// <copyright file="IFittedCurve{TX,TY}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Math
{
    /// <summary>
    ///     interface for a curve
    /// </summary>
    /// <typeparam name="TX">X dimension data type</typeparam>
    /// <typeparam name="TY">Y dimension data type</typeparam>
    public interface IFittedCurve<TX, TY> : ICurve<TX, TY>
        where TX : notnull
        where TY : notnull
    {
        /// <summary>
        /// Gets the closest y value for a given x value
        /// </summary>
        /// <param name="x">x value</param>
        /// <returns>closest y value</returns>
        TY Nearest(TX x);
    }
}