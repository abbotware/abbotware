// -----------------------------------------------------------------------
// <copyright file="IReceived{TContent}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Configuration
{
    /// <summary>
    /// Interface for recieved message content and evelope
    /// </summary>
    /// <typeparam name="TContent">content type</typeparam>
    public interface IReceived<TContent>
    {
        /// <summary>
        /// Gets the message content
        /// </summary>
        TContent Content { get; }

        /// <summary>
        /// Gets the message envelope
        /// </summary>
        IMessageEnvelope Envelope { get; }
    }
}