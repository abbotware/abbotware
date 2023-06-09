// -----------------------------------------------------------------------
// <copyright file="IRecordUpdater.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Internal
{
    using Amazon.TimestreamWrite.Model;

    /// <summary>
    /// Interface that updates an existing Timestream Record
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public interface IRecordUpdater<TMessage>
        where TMessage : notnull
    {
        /// <summary>
        /// Update the record with the given message
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="record">record to update</param>
        void Update(TMessage message, Record record);
    }
}