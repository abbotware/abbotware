// -----------------------------------------------------------------------
// <copyright file="IStorageManager.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    /// <summary>
    ///     interface that manages reading / writing of storage blocks
    /// </summary>
    public interface IStorageManager
    {
        /// <summary>
        ///     reads blocks from the storage manager for a given channel and type id
        /// </summary>
        /// <param name="blockId">guid for the block</param>
        /// <returns>returns task fetching the block</returns>
        Task<IStorageBlock> Read(Guid blockId);

        /// <summary>
        ///     Transfers a block into the storage manager
        /// </summary>
        /// <param name="block">storage block to write. Will be set to null after operation is complete</param>
        /// <returns>a task object bound to this write operation</returns>
        [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Will pass ownership of object into manager")]
        Task Write(ref IStorageBlock block);

        /// <summary>
        ///     Subscribes to a channel for a specific type
        /// </summary>
        /// <param name="channelId">data channel id</param>
        /// <param name="typeId">data type id</param>
        /// <returns>subscription to data channale/type</returns>
        IObservable<IStorageBlock> Subscribe(Guid? channelId, Guid? typeId);
    }
}