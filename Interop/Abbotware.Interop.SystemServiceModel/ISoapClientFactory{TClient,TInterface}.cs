// -----------------------------------------------------------------------
// <copyright file="ISoapClientFactory{TClient,TInterface}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.SystemServiceModel
{
    using System;
    using global::System.ServiceModel;

    /// <summary>
    /// interface for creating strongly typed soap client
    /// </summary>
    /// <typeparam name="TClient">auto generated proxy class type</typeparam>
    /// <typeparam name="TInterface">auto generated proxy interface type</typeparam>
    public interface ISoapClientFactory<TClient, TInterface>
        where TInterface : class
        where TClient : ClientBase<TInterface>
    {
        /// <summary>
        /// Creates a soap client for a given uri
        /// </summary>
        /// <param name="uri">uri</param>
        /// <returns>configured client</returns>
        TClient Create(Uri uri);

        /// <summary>
        /// Creates a soap client for a given endpoint
        /// </summary>
        /// <param name="endpoint">endpoint address</param>
        /// <returns>configured client</returns>
        TClient Create(EndpointAddress endpoint);
    }
}
