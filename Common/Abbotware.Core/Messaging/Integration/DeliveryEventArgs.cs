// -----------------------------------------------------------------------
// <copyright file="DeliveryEventArgs.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration
{
    using System;
    using Abbotware.Core.Messaging.Integration.Configuration;

    /// <summary>
    /// Delivery Args
    /// </summary>
    public class DeliveryEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryEventArgs"/> class.
        /// </summary>
        /// <param name="envelope">message envelope</param>
        public DeliveryEventArgs(IMessageEnvelope envelope)
        {
            this.Envelope = envelope;
        }

        /// <summary>
        /// Gets the envelope
        /// </summary>
        public IMessageEnvelope Envelope { get; }
    }
}
