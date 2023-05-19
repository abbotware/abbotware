// -----------------------------------------------------------------------
// <copyright file="IReadOnlyStatisticCollection{TKey1,TKey2}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Collections
{
    using System;

    /// <summary>
    /// Generalized statistic information (ReadOnly)
    /// </summary>
    /// <typeparam name="TKey1">key type 1</typeparam>
    /// <typeparam name="TKey2">key type 2</typeparam>
    public interface IReadOnlyStatisticCollection<TKey1, TKey2> : IReadOnlyStatisticCollection<Tuple<TKey1, TKey2>>
    {
    }
}