// -----------------------------------------------------------------------
// <copyright file="StringCappedList.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Redis.Collections
{
    using Abbotware.Interop.Redis.ExtensionPoints;
    using StackExchange.Redis;

    /// <summary>
    /// Redis 'Capped' List for strings
    /// </summary>
    public class StringCappedList : BaseCappedList<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringCappedList"/> class.
        /// </summary>
        /// <param name="remotekey">remote key name</param>
        /// <param name="capacity">capacity of the list</param>
        /// <param name="database">redis database</param>
        public StringCappedList(string remotekey, int capacity, IDatabase database)
            : base(remotekey, capacity, database)
        {
        }

        /// <inheritdoc />
        protected override string OnConvertElement(RedisValue value)
        {
            return value!;
        }

        /// <inheritdoc />
        protected override RedisValue OnConvertElement(string value)
        {
            return value;
        }
    }
}