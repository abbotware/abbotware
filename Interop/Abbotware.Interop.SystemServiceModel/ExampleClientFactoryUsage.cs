// -----------------------------------------------------------------------
// <copyright file="ExampleClientFactoryUsage.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.SystemServiceModel
{
    using System;
    using System.ServiceModel;
    using Example;

    /// <summary>
    /// Example Client Factory
    /// </summary>
    public class ExampleClientFactoryUsage : ISoapClientFactory<BLZServicePortTypeClient, BLZServicePortType>
    {
        /// <inheritdoc/>
        public BLZServicePortTypeClient Create(Uri uri)
        {
            return this.Create(new EndpointAddress(uri));
        }

        /// <inheritdoc/>
        public BLZServicePortTypeClient Create(EndpointAddress endpoint)
        {
            // set options if needed
            var binding = new BasicHttpsBinding();

            return new BLZServicePortTypeClient(binding, endpoint);
        }
    }
}
