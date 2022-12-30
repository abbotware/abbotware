// -----------------------------------------------------------------------
// <copyright file="BaseKeyValueStore{TNative}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache.ExtensionPoints
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Abbotware.Core.Collections;

    /// <summary>
    /// Base ckass for an encoded key value collection
    /// </summary>
    /// <typeparam name="TNative">native key value</typeparam>
    public abstract class BaseKeyValueStore<TNative> : IEncodedKeyValueStore
    {
        private readonly Dictionary<string, TNative> kv = new();

        /// <summary>
        /// Gets the key names
        /// </summary>
        public IEnumerable<string> Fields => this.kv.Keys;

        /// <summary>
        /// Gets the native key values
        /// </summary>
        public IEnumerable<TNative> Values => this.kv.Values;

        /// <summary>
        /// Gets a key value pair list of the key/values
        /// </summary>
        public IEnumerable<KeyValuePair<string, TNative>> KeyValues => this.kv.ToList();

        /// <inheritdoc/>
        public void EncodeInt32(string name, int? value)
        {
            this.VerifyKeyDoesNotExist(name);

            if (!value.HasValue)
            {
                return;
            }

            this.kv.Add(name, this.OnEncodeInt32(name, value.Value));
        }

        /// <inheritdoc/>
        public void EncodeBoolean(string name, bool? value)
        {
            this.VerifyKeyDoesNotExist(name);

            if (!value.HasValue)
            {
                return;
            }

            this.kv.Add(name, this.OnEncodeBoolean(name, value.Value));
        }

        /// <inheritdoc/>
        public bool? DecodeBoolean(string name)
        {
            this.VerifyKeyExists(name);

            return this.OnDecodeBoolean(name, this.kv[name]);
        }

        /// <inheritdoc/>
        public int? DecodeInt32(string name)
        {
            this.VerifyKeyExists(name);

            return this.OnDecodeInt32(name, this.kv[name]);
        }

        /// <inheritdoc/>
        public void EncodeString(string name, string? value)
        {
            this.VerifyKeyDoesNotExist(name);

            if (value == null)
            {
                return;
            }

            this.kv.Add(name, this.OnEncodeString(name, value));
        }

        /// <inheritdoc/>
        public void EncodeGuid(string name, Guid? value)
        {
            this.VerifyKeyDoesNotExist(name);

            if (!value.HasValue)
            {
                return;
            }

            this.kv.Add(name, this.OnEncodeGuid(name, value.Value));
        }

        /// <inheritdoc/>
        public string DecodeString(string name)
        {
            this.VerifyKeyExists(name);

            return this.OnDecodeString(name, this.kv[name]);
        }

        /// <inheritdoc/>
        public Guid? DecodeGuid(string name)
        {
            this.VerifyKeyExists(name);

            return this.OnDecodeGuid(name, this.kv[name]);
        }

        /// <inheritdoc/>
        public void EncodeTimeSpan(string name, TimeSpan? value)
        {
            this.VerifyKeyDoesNotExist(name);

            if (!value.HasValue)
            {
                return;
            }

            this.kv.Add(name, this.OnEncodeTimeSpan(name, value.Value));
        }

        /// <inheritdoc/>
        public TimeSpan? DecodeTimeSpan(string name)
        {
            this.VerifyKeyExists(name);

            return this.OnDecodeTimeSpan(name, this.kv[name]);
        }

        /// <inheritdoc/>
        public void EncodeInt64(string name, long? value)
        {
            this.VerifyKeyDoesNotExist(name);

            if (!value.HasValue)
            {
                return;
            }

            this.kv.Add(name, this.OnEncodeInt64(name, value.Value));
        }

        /// <inheritdoc/>
        public long? DecodeInt64(string name)
        {
            this.VerifyKeyExists(name);

            return this.OnDecodeInt64(name, this.kv[name]);
        }

        /// <inheritdoc/>
        public void EncodeDateTimeOffset(string name, DateTimeOffset? value)
        {
            this.VerifyKeyDoesNotExist(name);

            if (!value.HasValue)
            {
                return;
            }

            this.kv.Add(name, this.OnEncodeDateTimeOffset(name, value.Value));
        }

        /// <inheritdoc/>
        public DateTimeOffset? DecodeDateTimeOffset(string name)
        {
            this.VerifyKeyExists(name);

            return this.OnDecodeDateTimeOffset(name, this.kv[name]);
        }

        /// <inheritdoc/>
        public void EncodeUtcDateTime(string name, DateTime? value)
        {
            this.VerifyKeyDoesNotExist(name);

            if (!value.HasValue)
            {
                return;
            }

            if (value.Value.Kind == DateTimeKind.Unspecified)
            {
                throw new InvalidOperationException("DateTime kind is not specified.  Can't Convert to UTC");
            }

            if (value.Value.Kind == DateTimeKind.Local)
            {
                value = value.Value.ToUniversalTime();
            }

            this.kv.Add(name, this.OnEncodeUtcDateTime(name, value.Value));
        }

        /// <inheritdoc/>
        public DateTime? DecodeUtcDateTime(string name)
        {
            this.VerifyKeyExists(name);

            var dt = this.OnDecodeUtcDateTime(name, this.kv[name]);

            if (dt.Kind != DateTimeKind.Utc)
            {
                throw new InvalidOperationException("DateTime is not specified as UTC");
            }

            return dt;
        }

        /// <inheritdoc/>
        public void EncodeEnum<TEnum>(string name, TEnum? value)
            where TEnum : struct, IComparable
        {
            this.VerifyKeyDoesNotExist(name);

            if (!value.HasValue)
            {
                return;
            }

            var e = value.Value as Enum;

            var intValue = Convert.ToInt32(e, CultureInfo.InvariantCulture);

            this.EncodeInt32(name, intValue);
        }

        /// <inheritdoc/>
        public TEnum? DecodeEnum<TEnum>(string name)
            where TEnum : struct, IComparable
        {
            var intValue = this.DecodeInt32(name);

            return (TEnum?)(object?)intValue;
        }

        /// <inheritdoc/>
        public IEncodedKeyValueStore DecodeKeyValueCollection(string name)
        {
            this.VerifyKeyExists(name);

            return this.OnDecodeKeyValueCollection(name, this.kv[name]);
        }

        /// <summary>
        /// hook to decode kv collection
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to decode</param>
        /// <returns>decoded value</returns>
        protected abstract IEncodedKeyValueStore OnDecodeKeyValueCollection(string name, TNative value);

        /// <summary>
        /// hook to encode 32 bit int
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to encode</param>
        /// <returns>encoded value</returns>
        protected abstract TNative OnEncodeInt32(string name, int value);

        /// <summary>
        /// hook to decode 32 bit int
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to decode</param>
        /// <returns>decoded value</returns>
        protected abstract int OnDecodeInt32(string name, TNative value);

        /// <summary>
        /// hook to encode Boolean
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to encode</param>
        /// <returns>encoded value</returns>
        protected abstract TNative OnEncodeBoolean(string name, bool value);

        /// <summary>
        /// hook to decode Boolean
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to decode</param>
        /// <returns>decoded value</returns>
        protected abstract bool OnDecodeBoolean(string name, TNative value);

        /// <summary>
        /// hook to encode string
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to encode</param>
        /// <returns>encoded value</returns>
        protected abstract TNative OnEncodeString(string name, string value);

        /// <summary>
        /// hook to decode string
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to decode</param>
        /// <returns>decoded value</returns>
        protected abstract string OnDecodeString(string name, TNative value);

        /// <summary>
        /// hook to encode Guid
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to encode</param>
        /// <returns>encoded value</returns>
        protected abstract TNative OnEncodeGuid(string name, Guid value);

        /// <summary>
        /// hook to decode Guid
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to decode</param>
        /// <returns>decoded value</returns>
        protected abstract Guid OnDecodeGuid(string name, TNative value);

        /// <summary>
        /// hook to encode TimeSpan
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to encode</param>
        /// <returns>encoded value</returns>
        protected abstract TNative OnEncodeTimeSpan(string name, TimeSpan value);

        /// <summary>
        /// hook to decode TimeSpan
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to decode</param>
        /// <returns>decoded value</returns>
        protected abstract TimeSpan OnDecodeTimeSpan(string name, TNative value);

        /// <summary>
        /// hook to encode 64 bit int
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to encode</param>
        /// <returns>encoded value</returns>
        protected abstract TNative OnEncodeInt64(string name, long value);

        /// <summary>
        /// hook to decode 64 bit int
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to decode</param>
        /// <returns>decoded value</returns>
        protected abstract long OnDecodeInt64(string name, TNative value);

        /// <summary>
        /// hook to encode DateTimeOffset
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to encode</param>
        /// <returns>encoded value</returns>
        protected abstract TNative OnEncodeDateTimeOffset(string name, DateTimeOffset value);

        /// <summary>
        /// hook to decode DateTimeOffset
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to decode</param>
        /// <returns>decoded value</returns>
        protected abstract DateTimeOffset OnDecodeDateTimeOffset(string name, TNative value);

        /// <summary>
        /// hook to encode DateTime (UTC)
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to encode</param>
        /// <returns>encoded value</returns>
        protected abstract TNative OnEncodeUtcDateTime(string name, DateTime value);

        /// <summary>
        /// hook to decode DateTime (UTC)
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to decode</param>
        /// <returns>decoded value</returns>
        protected abstract DateTime OnDecodeUtcDateTime(string name, TNative value);

        /// <summary>
        /// Verifies key name exists, otherwise throw exception
        /// </summary>
        /// <param name="key">key name</param>
        protected void VerifyKeyExists(string key)
        {
            if (!this.kv.ContainsKey(key))
            {
                throw new KeyNotFoundException($"{key}");
            }
        }

        /// <summary>
        /// Verifies key name does not exist, otherwise throw exception
        /// </summary>
        /// <param name="key">key name</param>
        protected void VerifyKeyDoesNotExist(string key)
        {
            if (this.kv.ContainsKey(key))
            {
                throw new InvalidOperationException($"key:{key} already exists");
            }
        }

        /// <summary>
        /// Adds key value to internal dictionary
        /// </summary>
        /// <param name="key">key name</param>
        /// <param name="native">native key value</param>
        protected void AddValue(string key, TNative native)
        {
            this.kv.Add(key, native);
        }
    }
}