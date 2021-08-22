// -----------------------------------------------------------------------
// <copyright file="History.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System;

    /// <summary>
    /// History Range Specification
    /// </summary>
    public class History
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="History"/> class.
        /// </summary>
        /// <param name="periodType">period type</param>
        /// <param name="period">number of periods</param>
        /// <param name="frequencyType">frequency type</param>
        /// <param name="frequency">frequency rate</param>
        internal History(PeriodType? periodType, ushort? period, FrequencyType? frequencyType, ushort? frequency)
        {
            this.PeriodType = periodType;
            this.Period = period;
            this.FrequencyType = frequencyType;
            this.Frequency = frequency;
        }

        /// <summary>
        /// Gets the period type
        /// </summary>
        public PeriodType? PeriodType { get; }

        /// <summary>
        /// Gets the period
        /// </summary>
        public ushort? Period { get; }

        /// <summary>
        /// Gets the frequency type
        /// </summary>
        public FrequencyType? FrequencyType { get; }

        /// <summary>
        /// Gets the frequency rate
        /// </summary>
        public ushort? Frequency { get; }

        /// <summary>
        /// Creates the default range for days
        /// </summary>
        /// <returns>configured range object</returns>
        public static History Days()
        {
            return new History(Models.PeriodType.Day, null, null, null);
        }

        /// <summary>
        /// Creates the range for day with the default minute rate
        /// </summary>
        /// <param name="days">period count</param>
        /// <returns>configured range object</returns>
        public static History Days(HowManyDays days)
        {
            return new History(Models.PeriodType.Day, (ushort)days, null, null);
        }

        /// <summary>
        /// Creates the range for days
        /// </summary>
        /// <param name="days">period count</param>
        /// <param name="frequncy">frequency rate</param>
        /// <returns>configured range object</returns>
        public static History Days(HowManyDays days, Minutes frequncy)
        {
            return Create(Models.PeriodType.Day, (ushort?)days, Models.FrequencyType.Minute, (ushort?)frequncy);
        }

        /// <summary>
        /// Creates the default range for months
        /// </summary>
        /// <returns>configured range object</returns>
        public static History Months()
        {
            return Create(Models.PeriodType.Month, null, null, null);
        }

        /// <summary>
        /// Creates the range for months with the default frequncy rate
        /// </summary>
        /// <param name="months">period count</param>
        /// <returns>configured range object</returns>
        public static History Months(HowManyMonths months)
        {
            return Create(Models.PeriodType.Month, (ushort?)months, null, null);
        }

        /// <summary>
        /// Creates the range for months with the default frequncy rate
        /// </summary>
        /// <param name="months">period count</param>
        /// <param name="frequncy">frequency rate</param>
        /// <returns>configured range object</returns>
        public static History Months(HowManyMonths months, Monthly frequncy)
        {
            var ft = frequncy switch
            {
                Monthly.ByDay => Models.FrequencyType.Daily,
                Monthly.ByWeek => Models.FrequencyType.Weekly,
                _ => throw new NotImplementedException(),
            };

            return Create(Models.PeriodType.Month, (ushort?)months, ft, null);
        }

        /// <summary>
        /// Creates the default range for years
        /// </summary>
        /// <returns>configured range object</returns>
        public static History Years()
        {
            return Create(Models.PeriodType.Year, null, null, null);
        }

        /// <summary>
        /// Creates the range for years with the default frequncy rate
        /// </summary>
        /// <param name="years">period count</param>
        /// <returns>configured range object</returns>
        public static History Years(HowManyYears years)
        {
            return Create(Models.PeriodType.Year, (ushort?)years, null, null);
        }

        /// <summary>
        /// Creates the range for years with the specified frequncy rate
        /// </summary>
        /// <param name="years">period count</param>
        /// <param name="frequncy">frequency rate</param>
        /// <returns>configured range object</returns>
        public static History Years(HowManyYears years, Yearly frequncy)
        {
            var ft = frequncy switch
            {
                Yearly.ByDay => Models.FrequencyType.Daily,
                Yearly.ByWeek => Models.FrequencyType.Weekly,
                Yearly.ByMonth => Models.FrequencyType.Monthly,
                _ => throw new NotImplementedException(),
            };

            return Create(Models.PeriodType.Year, (ushort?)years, ft, null);
        }

        /// <summary>
        /// Creates the default range for year to date
        /// </summary>
        /// <returns>configured range object</returns>
        public static History YearToDate()
        {
            return Create(Models.PeriodType.YearToDate, null, null, null);
        }

        /// <summary>
        /// Creates the range for year to date with the specified frequency rate
        /// </summary>
        /// <param name="frequncy">frequency rate</param>
        /// <returns>configured range object</returns>
        public static History YearToDate(YearToDateRate frequncy)
        {
            var ft = frequncy switch
            {
                YearToDateRate.ByDay => Models.FrequencyType.Daily,
                YearToDateRate.ByWeek => Models.FrequencyType.Weekly,
                _ => throw new NotImplementedException(),
            };

            return Create(Models.PeriodType.YearToDate, null, ft, null);
        }

        private static History Create(PeriodType? periodType, ushort? period, FrequencyType? frequencyType, ushort? frequency)
        {
            return new History(periodType, period, frequencyType, frequency);
        }
    }
}
