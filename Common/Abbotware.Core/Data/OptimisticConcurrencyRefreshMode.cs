// -----------------------------------------------------------------------
// <copyright file="OptimisticConcurrencyRefreshMode.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data
{
    /// <summary>
    ///     Enumeration used to specify the concurrency refresh mode
    /// </summary>
    public enum OptimisticConcurrencyRefreshMode
    {
        /// <summary>
        ///     no concurrency mode specified, will throw an exception on concurrency issue
        /// </summary>
        None,

        /// <summary>
        ///     client changes win in conflict resolution
        /// </summary>
        Client,

        /// <summary>
        ///     database wins in conflict resolution
        /// </summary>
        Server,
    }
}