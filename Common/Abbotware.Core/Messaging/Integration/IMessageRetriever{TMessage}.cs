// -----------------------------------------------------------------------
// <copyright file="IMessageRetriever{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration
{
    using System.Threading.Tasks;
    using Abbotware.Core.Messaging.Integration.Configuration;

    /// <summary>
    ///     Interface that manages messages of a single type
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public interface IMessageRetriever<TMessage>
    {
        /// <summary>
        ///     Retrieves a message with the specified type
        /// </summary>
        /// <returns>received message</returns>
        Task<IReceived<TMessage>> RetrieveAsync();
    }
}