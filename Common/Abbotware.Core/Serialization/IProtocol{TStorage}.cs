// -----------------------------------------------------------------------
// <copyright file="IProtocol{TStorage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Serialization
{
    /// <summary>
    ///     Generic interface for encoding / decoding objects into storage
    /// </summary>
    /// <typeparam name="TStorage">protocol storage type</typeparam>
    public interface IProtocol<TStorage> : IProtocol<TStorage, TStorage>
    {
    }

    /// <summary>
    ///     Generic interface for encoding / decoding objects into storage
    /// </summary>
    /// <typeparam name="TEncoderStorage">encoder storage type</typeparam>
    /// <typeparam name="TDecoderStorage">decoder storage type</typeparam>
    public interface IProtocol<TEncoderStorage, TDecoderStorage> : IEncode<TEncoderStorage>, IDecode<TDecoderStorage>
    {
    }
}