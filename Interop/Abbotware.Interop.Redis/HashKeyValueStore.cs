// -----------------------------------------------------------------------
// <copyright file="HashKeyValueCollection.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Redis
{
    using System;
    using System.Globalization;
    using Abbotware.Core;
    using Abbotware.Core.Cache.ExtensionPoints;
    using Abbotware.Core.Collections;
    using Abbotware.Core.Extensions;
    using StackExchange.Redis;

    /// <summary>
    /// Key Value collection encoded into a Redis "Hash Key"
    /// </summary>
    /// <remarks>https://redis.io/topics/data-types (see Hashes)</remarks>
    public class HashKeyValueStore : BaseKeyValueStore<HashEntry>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HashKeyValueStore"/> class.
        /// </summary>
        public HashKeyValueStore()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HashKeyValueStore"/> class.
        /// </summary>
        /// <param name="keyvalues">redis HashEntry / key value pairs</param>
        public HashKeyValueStore(HashEntry[] keyvalues)
        {
            keyvalues = Arguments.EnsureNotNull(keyvalues, nameof(keyvalues));

            foreach (var kv in keyvalues)
            {
                this.AddValue(kv.Name, kv);
            }
        }

        /// <inheritdoc/>
        protected override HashEntry OnEncodeInt32(string name, int value)
        {
            return new HashEntry(name, value);
        }

        /// <inheritdoc/>
        protected override int OnDecodeInt32(string name, HashEntry value)
        {
            return (int)value.Value;
        }

        /// <inheritdoc/>
        protected override HashEntry OnEncodeBoolean(string name, bool value)
        {
            return new HashEntry(name, value);
        }

        /// <inheritdoc/>
        protected override bool OnDecodeBoolean(string name, HashEntry value)
        {
            return (bool)value.Value;
        }

        /// <inheritdoc/>
        protected override HashEntry OnEncodeString(string name, string value)
        {
            return new HashEntry(name, value);
        }

        /// <inheritdoc/>
        protected override string OnDecodeString(string name, HashEntry value)
        {
            return value.Value;
        }

        /// <inheritdoc/>
        protected override HashEntry OnEncodeGuid(string name, Guid value)
        {
            return new HashEntry(name, value.ToByteArray());
        }

        /// <inheritdoc/>
        protected override Guid OnDecodeGuid(string name, HashEntry value)
        {
            return new Guid((byte[])value.Value);
        }

        /// <inheritdoc/>
        protected override HashEntry OnEncodeTimeSpan(string name, TimeSpan value)
        {
            return this.OnEncodeInt64(name, value.Ticks);
        }

        /// <inheritdoc/>
        protected override TimeSpan OnDecodeTimeSpan(string name, HashEntry value)
        {
            return TimeSpan.FromTicks(this.OnDecodeInt64(name, value));
        }

        /// <inheritdoc/>
        protected override HashEntry OnEncodeInt64(string name, long value)
        {
            return new HashEntry(name, value);
        }

        /// <inheritdoc/>
        protected override long OnDecodeInt64(string name, HashEntry value)
        {
            return (long)value.Value;
        }

        /// <inheritdoc/>
        protected override HashEntry OnEncodeDateTimeOffset(string name, DateTimeOffset value)
        {
            return this.OnEncodeString(name, value.ToIso8601WithPrecision());
        }

        /// <inheritdoc/>
        protected override DateTimeOffset OnDecodeDateTimeOffset(string name, HashEntry value)
        {
            return DateTimeOffset.Parse(this.OnDecodeString(name, value), CultureInfo.InvariantCulture);
        }

        /// <inheritdoc/>
        protected override HashEntry OnEncodeUtcDateTime(string name, DateTime value)
        {
            return this.OnEncodeInt64(name, value.Ticks);
        }

        /// <inheritdoc/>
        protected override DateTime OnDecodeUtcDateTime(string name, HashEntry value)
        {
            return new DateTime(this.OnDecodeInt64(name, value), DateTimeKind.Utc);
        }

        /// <inheritdoc/>
        protected override IEncodedKeyValueStore OnDecodeKeyValueCollection(string name, HashEntry value)
        {
            throw new NotImplementedException();
        }
    }
}
