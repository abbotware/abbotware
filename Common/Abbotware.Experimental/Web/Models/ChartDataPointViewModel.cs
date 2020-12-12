// -----------------------------------------------------------------------
// <copyright file="ChartDataPointViewModel.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Web.Models
{
    /// <summary>
    ///     view model for chart visualizations
    /// </summary>
    /// <typeparam name="TCategory">type of category</typeparam>
    /// <typeparam name="TValue">type of data point</typeparam>
    public class ChartDataPointViewModel<TCategory, TValue>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ChartDataPointViewModel{TCategory, TValue}" /> class.
        /// </summary>
        /// <param name="category">category value</param>
        /// <param name="value">data value</param>
        public ChartDataPointViewModel(TCategory category, TValue value)
            : this(category, value, string.Empty)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChartDataPointViewModel{TCategory, TValue}" /> class.
        /// </summary>
        /// <param name="category">category value</param>
        /// <param name="value">data value</param>
        /// <param name="color">color value</param>
        public ChartDataPointViewModel(TCategory category, TValue value, string color)
        {
            this.Category = category;
            this.Value = value;
            this.Color = color;
        }

        /// <summary>
        ///     Gets the category value
        /// </summary>
        public TCategory Category { get; }

        /// <summary>
        ///     Gets the data value
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        ///     Gets the color value
        /// </summary>
        public string Color { get; }
    }
}