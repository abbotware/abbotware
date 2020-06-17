// -----------------------------------------------------------------------
// <copyright file="IBasicConnection.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration
{
    using Abbotware.Core.Objects;

    /// <summary>
    /// Interface for a basic messaging connection
    /// </summary>
    public interface IBasicConnection : IConnection
    {
        /// <summary>
        ///     creates a retiever
        /// </summary>
        /// <returns>retiever</returns>
        IBasicRetriever CreateRetriever();

        /// <summary>
        ///     creates a publisher
        /// </summary>
        /// <returns>publisher</returns>
        IBasicPublisher CreatePublisher();

        /// <summary>
        ///     creates a consumer
        /// </summary>
        /// <returns>consumer</returns>
        IBasicConsumer CreateConsumer();

        /// <summary>
        ///     creates a retiever
        /// </summary>
        /// <typeparam name="TRetriever">retriever type</typeparam>
        /// <returns>retiever</returns>
        TRetriever CreateRetriever<TRetriever>()
            where TRetriever : class, IBasicRetriever;

        /// <summary>
        ///     creates a publisher
        /// </summary>
        /// <typeparam name="TPublisher">publisher type</typeparam>
        /// <returns>publisher</returns>
        TPublisher CreatePublisher<TPublisher>()
              where TPublisher : class, IBasicPublisher;

        /// <summary>
        ///     creates a consumer
        /// </summary>
        /// <typeparam name="TConsumer">consumer type</typeparam>
        /// <returns>consumer</returns>
        TConsumer CreateConsumer<TConsumer>()
            where TConsumer : class, IBasicConsumer;
    }
}