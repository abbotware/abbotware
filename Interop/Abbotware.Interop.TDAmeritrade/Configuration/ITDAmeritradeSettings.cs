// -----------------------------------------------------------------------
// <copyright file="ITDAmeritradeSettings.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Configuration
{
    using Abbotware.Interop.RestSharp.Configuration;

    /// <summary>
    /// Readonly API Settings for TD Ameritrade Client
    /// </summary>
    public interface ITDAmeritradeSettings : IApiSettings
    {
        /// <summary>
        /// Gets the Bearer token
        /// </summary>
        string? BearerToken { get; }
    }
}