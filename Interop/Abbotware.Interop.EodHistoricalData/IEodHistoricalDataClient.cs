// -----------------------------------------------------------------------
// <copyright file="IEodHistoricalDataClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData
{
    using System;

    /// <summary>
    /// Interface for EOD Historical Data API Client
    /// </summary>
    public interface IEodHistoricalDataClient : IFundamentalClient, IExchangeClient, IDisposable
    {
        /// <summary>
        /// Gets the fundamentals api subset
        /// </summary>
        IFundamentalClient Fundamentals { get; }

        /// <summary>
        /// Gets the exchange api subset
        /// </summary>
        IExchangeClient Exchanges { get; }
    }
}