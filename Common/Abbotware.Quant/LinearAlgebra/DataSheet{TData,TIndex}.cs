// -----------------------------------------------------------------------
// <copyright file="DataFrame{TData,TIndex}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.LinearAlgebra
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// A data frame
    /// </summary>
    /// <typeparam name="TData">data element type</typeparam>
    /// <typeparam name="TIndex">index element type</typeparam>
    public class DataSheet<TData, TIndex> : Matrix<TData>
        where TData : INumber<TData>
        where TIndex : IComparable<TIndex>
    {
        private readonly List<TIndex> index;
        private readonly List<string> labels;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSheet{TData, TIndex}"/> class.
        /// </summary>
        /// <param name="m">jagged arrays</param>
        /// <param name="index">index values</param>
        /// <param name="labels">labels</param>
        public DataSheet(TData[][] m, IEnumerable<TIndex> index, IEnumerable<string> labels)
            : base(m)
        {
            this.index = index.ToList();
            this.labels = labels.ToList();
        }
    }
}
