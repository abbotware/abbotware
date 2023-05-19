// -----------------------------------------------------------------------
// <copyright file="Received{TContent}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Configuration.Models
{
    /// <summary>
    ///     Message that was received
    /// </summary>
    /// <typeparam name="TContent">type of message</typeparam>
    public class Received<TContent> : IReceived<TContent>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Received{TMessage}" /> class.
        /// </summary>
        /// <param name="content">typed message data</param>
        /// <param name="envelope">message evenlope metadata</param>
        public Received(TContent content, IMessageEnvelope envelope)
        {
            Arguments.NotNull(content, nameof(content));
            Arguments.NotNull(envelope, nameof(envelope));

            this.Content = content;
            this.Envelope = envelope;
        }

        /// <summary>
        ///     Gets the messages that was received
        /// </summary>
        public TContent Content
        {
            get;
        }

        /// <summary>
        ///     Gets the message metadata that was received
        /// </summary>
        public IMessageEnvelope Envelope
        {
            get;
        }
    }
}