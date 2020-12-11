// -----------------------------------------------------------------------
// <copyright file="IEncodedKeyValueStore.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Collections
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// interface for encoding data into a strongled typed key value collection
    /// </summary>
    public interface IEncodedKeyValueStore
    {
        /// <summary>
        /// Gets the list of fields names
        /// </summary>
        IEnumerable<string> Fields { get; }

        /// <summary>
        /// Encodes a boolean
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to encode</param>
        void EncodeBoolean(string name, bool? value);

        /// <summary>
        /// Encodes a 32 bit int
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to encode</param>
        void EncodeInt32(string name, int? value);

        /// <summary>
        /// Encodes a 64 bit int
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to encode</param>
        void EncodeInt64(string name, long? value);

        /// <summary>
        /// Encodes a string
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to encode</param>
        void EncodeString(string name, string value);

        /// <summary>
        /// Encodes a Guid
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to encode</param>
        void EncodeGuid(string name, Guid? value);

        /// <summary>
        /// Decodes a boolean
        /// </summary>
        /// <param name="name">field name</param>
        /// <returns>decoded value</returns>
        bool DecodeBoolean(string name);

        /// <summary>
        /// Decodes a 32 bit int
        /// </summary>
        /// <param name="name">field name</param>
        /// <returns>decoded value</returns>
        int DecodeInt32(string name);

        /// <summary>
        /// Decodes a 64 bit int
        /// </summary>
        /// <param name="name">field name</param>
        /// <returns>decoded value</returns>
        long DecodeInt64(string name);

        /// <summary>
        /// Decodes a string
        /// </summary>
        /// <param name="name">field name</param>
        /// <returns>decoded value</returns>
        string DecodeString(string name);

        /// <summary>
        /// Decodes a nested key value collection
        /// </summary>
        /// <param name="name">field name</param>
        /// <returns>decoded key value collection</returns>
        IEncodedKeyValueStore DecodeKeyValueCollection(string name);

        /// <summary>
        /// Decodes a Guid
        /// </summary>
        /// <param name="name">field name</param>
        /// <returns>decoded value</returns>
        Guid DecodeGuid(string name);

        /// <summary>
        /// Encodes a TimeSpan
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to encode</param>
        void EncodeTimeSpan(string name, TimeSpan? value);

        /// <summary>
        /// Decodes a TimeSpan
        /// </summary>
        /// <param name="name">field name</param>
        /// <returns>decoded value</returns>
        TimeSpan DecodeTimeSpan(string name);

        /// <summary>
        /// Encodes a DateTimeOffset
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to encode</param>
        void EncodeDateTimeOffset(string name, DateTimeOffset? value);

        /// <summary>
        /// Decodes a DateTimeOffset
        /// </summary>
        /// <param name="name">field name</param>
        /// <returns>decoded value</returns>
        DateTimeOffset DecodeDateTimeOffset(string name);

        /// <summary>
        /// Encodes a DateTime (UTC)
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">value to encode</param>
        void EncodeUtcDateTime(string name, DateTime? value);

        /// <summary>
        /// Decodes a DateTime (UTC)
        /// </summary>
        /// <param name="name">field name</param>
        /// <returns>decoded value</returns>
        DateTime DecodeUtcDateTime(string name);

        /// <summary>
        /// Encodes a enum
        /// </summary>
        /// <typeparam name="TEnum">enum type</typeparam>
        /// <param name="name">field name</param>
        /// <param name="value">value to encode</param>
        void EncodeEnum<TEnum>(string name, TEnum? value)
            where TEnum : struct, IComparable;

        /// <summary>
        /// Decodes an enum
        /// </summary>
        /// <typeparam name="TEnum">enum type</typeparam>
        /// <param name="name">field name</param>
        /// <returns>decoded value</returns>
        TEnum DecodeEnum<TEnum>(string name)
            where TEnum : struct, IComparable;
    }
}