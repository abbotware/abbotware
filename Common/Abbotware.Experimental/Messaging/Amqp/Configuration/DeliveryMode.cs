// -----------------------------------------------------------------------
// <copyright file="DeliveryMode.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.Configuration
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     Type of delivery mode for the message
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "following 3rd party spec")]
    [SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "third party library uses byte")]
    public enum DeliveryMode : byte
    {
        /// <summary>
        ///     Message will not be stored to disk
        /// </summary>
        Transient = 1,

        /// <summary>
        ///     Message will be persisted to disk (if exchange / queue supports it)
        /// </summary>
        Persistent = 2,
    }
}