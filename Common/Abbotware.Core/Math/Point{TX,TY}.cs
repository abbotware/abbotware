// -----------------------------------------------------------------------
// <copyright file="Point{TX,TY}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Math
{
    /// <summary>
    ///     Point with 2 dimensions
    /// </summary>
    /// <typeparam name="TX">X dimension data type</typeparam>
    /// <typeparam name="TY">Y dimension data type</typeparam>
    public record class Point<TX, TY>(TX X, TY Y) : IPoint<TX, TY>
        where TX : notnull
        where TY : notnull
    {
    }
}