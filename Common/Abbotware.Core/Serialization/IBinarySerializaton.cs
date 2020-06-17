// -----------------------------------------------------------------------
// <copyright file="IBinarySerializaton.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Serialization
{
    using System;

    /// <summary>
    /// protocol type for serialization of objects into byte arrays
    /// </summary>
    public interface IBinarySerializaton : ISerialization<byte[]>, ISerialization<ReadOnlyMemory<byte>>
    {
    }
}