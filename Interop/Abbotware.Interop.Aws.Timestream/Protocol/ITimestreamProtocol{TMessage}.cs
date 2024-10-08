﻿// -----------------------------------------------------------------------
// <copyright file="ITimestreamProtocol{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol
{
    /// <summary>
    /// interface for decoding / encoding Timestream messages
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public interface ITimestreamProtocol<TMessage> : ITimestreamWriteProtocol<TMessage>, ITimestreamQueryProtocol<TMessage>
        where TMessage : notnull
    {
    }
}
