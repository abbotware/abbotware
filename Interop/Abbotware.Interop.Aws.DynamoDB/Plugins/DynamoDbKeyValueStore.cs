// -----------------------------------------------------------------------
// <copyright file="DynamoDbKeyValueStore.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.DynamoDB.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Abbotware.Core;
    using Abbotware.Core.Cache.ExtensionPoints;
    using Abbotware.Core.Collections;
    using Abbotware.Core.Extensions;
    using global::Amazon.DynamoDBv2.DocumentModel;

    /// <summary>
    /// Key Value collection encoded into a DynamoDb "document"
    /// </summary>
    public class DynamoDbKeyValueStore : BaseKeyValueStore<DynamoDBEntry>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamoDbKeyValueStore"/> class.
        /// </summary>
        public DynamoDbKeyValueStore()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamoDbKeyValueStore"/> class.
        /// </summary>
        /// <param name="document">DynamoDB Document</param>
        public DynamoDbKeyValueStore(Document document)
            : this(document.ToArray())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamoDbKeyValueStore"/> class.
        /// </summary>
        /// <param name="keyValues">DynamoDBEntry / key value pairs</param>
        public DynamoDbKeyValueStore(KeyValuePair<string, DynamoDBEntry>[] keyValues)
        {
            keyValues = Arguments.EnsureNotNull(keyValues, nameof(keyValues));

            foreach (var field in keyValues)
            {
                this.AddValue(field.Key, field.Value);
            }
        }

        /// <inheritdoc/>
        protected override DynamoDBEntry OnEncodeBoolean(string name, bool value)
        {
            if (value)
            {
                return new Primitive(1.ToString(CultureInfo.InvariantCulture), true);
            }
            else
            {
                return new Primitive(0.ToString(CultureInfo.InvariantCulture), true);
            }
        }

        /// <inheritdoc/>
        protected override bool OnDecodeBoolean(string name, DynamoDBEntry value)
        {
            value = Arguments.EnsureNotNull(value, nameof(value));

            return value.AsBoolean();
        }

        /// <inheritdoc/>
        protected override DynamoDBEntry OnEncodeInt32(string name, int value)
        {
            return new Primitive(value.ToString(CultureInfo.InvariantCulture), true);
        }

        /// <inheritdoc/>
        protected override int OnDecodeInt32(string name, DynamoDBEntry value)
        {
            value = Arguments.EnsureNotNull(value, nameof(value));

            return value.AsInt();
        }

        /// <inheritdoc/>
        protected override DynamoDBEntry OnEncodeString(string name, string value)
        {
            return new Primitive(value);
        }

        /// <inheritdoc/>
        protected override string OnDecodeString(string name, DynamoDBEntry value)
        {
            value = Arguments.EnsureNotNull(value, nameof(value));

            return value.AsString();
        }

        /// <inheritdoc/>
        protected override DynamoDBEntry OnEncodeGuid(string name, Guid value)
        {
            return new Primitive(value.ToByteArray());
        }

        /// <inheritdoc/>
        protected override Guid OnDecodeGuid(string name, DynamoDBEntry value)
        {
            value = Arguments.EnsureNotNull(value, nameof(value));

            return new Guid(value.AsByteArray());
        }

        /// <inheritdoc/>
        protected override DynamoDBEntry OnEncodeTimeSpan(string name, TimeSpan value)
        {
            return this.OnEncodeInt64(name, value.Ticks);
        }

        /// <inheritdoc/>
        protected override TimeSpan OnDecodeTimeSpan(string name, DynamoDBEntry value)
        {
            return TimeSpan.FromTicks(this.OnDecodeInt64(name, value));
        }

        /// <inheritdoc/>
        protected override DynamoDBEntry OnEncodeInt64(string name, long value)
        {
            return new Primitive(value.ToString(CultureInfo.InvariantCulture), true);
        }

        /// <inheritdoc/>
        protected override long OnDecodeInt64(string name, DynamoDBEntry value)
        {
            value = Arguments.EnsureNotNull(value, nameof(value));

            return value.AsLong();
        }

        /// <inheritdoc/>
        protected override DynamoDBEntry OnEncodeDateTimeOffset(string name, DateTimeOffset value)
        {
            return this.OnEncodeString(name, value.ToIso8601WithPrecision());
        }

        /// <inheritdoc/>
        protected override DateTimeOffset OnDecodeDateTimeOffset(string name, DynamoDBEntry value)
        {
            return DateTimeOffset.Parse(this.OnDecodeString(name, value), CultureInfo.InvariantCulture);
        }

        /// <inheritdoc/>
        protected override DynamoDBEntry OnEncodeUtcDateTime(string name, DateTime value)
        {
            return this.OnEncodeInt64(name, value.Ticks);
        }

        /// <inheritdoc/>
        protected override DateTime OnDecodeUtcDateTime(string name, DynamoDBEntry value)
        {
            return new DateTime(this.OnDecodeInt64(name, value), DateTimeKind.Utc);
        }

        /// <inheritdoc/>
        protected override IEncodedKeyValueStore OnDecodeKeyValueCollection(string name, DynamoDBEntry value)
        {
            value = Arguments.EnsureNotNull(value, nameof(value));

            var doc = value.AsDocument();

            return new DynamoDbKeyValueStore(doc);
        }
    }
}
