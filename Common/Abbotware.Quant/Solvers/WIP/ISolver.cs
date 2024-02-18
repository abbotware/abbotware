// -----------------------------------------------------------------------
// <copyright file="ISolver.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Solvers
{
    using System;
    using Abbotware.Core.Math;

    /// <summary>
    /// interface for a generic solver
    /// </summary>
    public interface ISolver
    {
        public double? Solve(Func<double, double> func, Interval<double> range, double target, double tolerance, uint maxIterations, double[]? trace);

        public double? Solve(Func<double, double> func, Interval<double> range);

    }
}
