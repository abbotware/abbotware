// -----------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// IEnumerable statistics methods
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Get basic Statistics about the populate data
        /// </summary>
        /// <typeparam name="T">numeric type</typeparam>
        /// <param name="data">data</param>
        /// <returns>stats</returns>
        /// <exception cref="ArgumentException">multi-modal notsupported</exception>
        public static PopulationStatistics<T> PopulationStatistics<T>(this IEnumerable<T> data)
                  where T : INumber<T>
        {
            var sorted = data.OrderBy(x => x).ToList();

            var buckets = sorted.GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.ToList())
                .OrderByDescending(x => x.Value.Count)
                .ToList();

            var first = buckets.First();

            if (buckets.Count > 1)
            {
                var second = buckets.Skip(1).First();

                if (first.Value == second.Value)
                {
                    throw new ArgumentException("Multimodal population");
                }
            }

            T mode = first.Key;
            T median = T.Zero;
            T sum = T.Zero;
            T product = T.One;
            T count = T.Zero;
            int i = 0;

            foreach (var x in sorted)
            {
                i++;

                if (i == sorted.Count / 2)
                {
                    median = x;
                }

                sum += x;
                count++;

                // for geometric mean
                product *= x;
            }

            var arithmeticMean = sum / count;

            return new PopulationStatistics<T>(arithmeticMean, default, default, median, mode);
        }
    }
}
