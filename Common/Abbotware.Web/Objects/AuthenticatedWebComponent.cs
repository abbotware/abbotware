// -----------------------------------------------------------------------
// <copyright file="AuthenticatedWebComponent.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Web.Objects
{
    using Abbotware.Core.Logging;
    using Abbotware.Web.Api.Configuration;

    /// <summary>
    ///     Component that uses a web client
    /// </summary>
    public abstract class AuthenticatedWebComponent : AuthenticatedWebComponent<IApiClientOptions>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AuthenticatedWebComponent" /> class.
        /// </summary>
        /// <param name="secret">secret</param>
        /// <param name="configuration">configuration</param>
        /// <param name="logger">injected logger</param>
        protected AuthenticatedWebComponent(string secret, IApiClientOptions configuration, ILogger logger)
            : base(secret, configuration, logger)
        {
        }
    }
}