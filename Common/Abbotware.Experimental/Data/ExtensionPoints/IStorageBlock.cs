// -----------------------------------------------------------------------
// <copyright file="IStorageBlock.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data
{
    using System;

    /// <summary>
    ///     interface for a basic unit of storage
    /// </summary>
    public interface IStorageBlock : IDisposable
    {
        /// <summary>
        ///     Gets the channel id for this block
        /// </summary>
        Guid Id { get; }

        /// <summary>
        ///     Gets the channel id for this block
        /// </summary>
        Guid ChannelId { get; }

        /// <summary>
        ///     Gets the type id for this block
        /// </summary>
        Guid TypeId { get; }

        /// <summary>
        ///     Gets the time stamp the block was received
        /// </summary>
        DateTimeOffset TimeReceived { get; }

        /// <summary>
        ///     Gets the raw binary data for this block
        /// </summary>
        UIntPtr Data { get; }
    }
}