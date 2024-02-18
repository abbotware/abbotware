// -----------------------------------------------------------------------
// <copyright file="PopulationStatistics.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Statistics
{
    using System.Numerics;

    /// <summary>
    /// Population Statistics
    /// </summary>
    /// <typeparam name="T">numeric type</typeparam>
    /// <param name="ArithmeticMean">Population Arithmetic Mean</param>
    /// <param name="GeometricMean">Population Geometric Mean</param>
    /// <param name="HarmonicMean">Population Harmonic Mean</param>
    /// <param name="Median">Population Median</param>
    /// <param name="Mode">Population Mode</param>
    public record class PopulationStatistics<T>(T ArithmeticMean, T? GeometricMean, T? HarmonicMean, T Median, T Mode)
         where T : INumber<T>;
}
