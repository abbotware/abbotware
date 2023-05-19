// -----------------------------------------------------------------------
// <copyright file="IAtomicCounter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Threading.Counters
{
    /// <summary>
    ///     interface for an object that can increment and decrement its value
    /// </summary>
    public interface IAtomicCounter : IAccumulator, IDecumulator
    {
    }
}