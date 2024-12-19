// -----------------------------------------------------------------------
// <copyright file="BaseBinarySerialization.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Serialization;

using System;

/// <summary>
/// base class for binary serialization
/// </summary>
public abstract class BaseBinarySerialization : IBinarySerializaton
{
    /// <inheritdoc />
    public abstract object? Decode(byte[] storage, Type type);

    /// <inheritdoc />
    public abstract byte[] Encode<T>(T value);

    /// <inheritdoc />
    public abstract T Decode<T>(byte[] storage);

    /// <inheritdoc />
    public object? Decode(ReadOnlyMemory<byte> storage, Type type)
        => this.Decode(storage.ToArray(), type);

    /// <inheritdoc />
    ReadOnlyMemory<byte> IEncode<ReadOnlyMemory<byte>>.Encode<T>(T value)
        => new(this.Encode(value));

    /// <inheritdoc />
    public T Decode<T>(ReadOnlyMemory<byte> storage)
        => this.Decode<T>(storage.ToArray());
}