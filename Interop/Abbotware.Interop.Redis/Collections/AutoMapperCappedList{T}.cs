// -----------------------------------------------------------------------
// <copyright file="AutoMapperCappedList{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Redis.Collections
{
    using Abbotware.Interop.Redis.ExtensionPoints;
    using global::AutoMapper;
    using StackExchange.Redis;

    /// <summary>
    /// Redis List with AutoMapper converters
    /// </summary>
    /// <typeparam name="T">List Element Type</typeparam>
    public class AutoMapperCappedList<T> : BaseCappedList<T>
    {
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperCappedList{T}"/> class.
        /// </summary>
        /// <param name="remotekey">remote key name</param>
        /// <param name="capacity">capacity of the list</param>
        /// <param name="database">redis database</param>
        /// <param name="mapper">type mapper</param>
        public AutoMapperCappedList(string remotekey, int capacity, IDatabase database, IMapper mapper)
            : base(remotekey, capacity, database)
        {
            this.mapper = mapper;
        }

        /// <inheritdoc />
        protected override T OnConvertElement(RedisValue value)
        {
            return this.mapper.Map<T>(value);
        }

        /// <inheritdoc />
        protected override RedisValue OnConvertElement(T value)
        {
            return this.mapper.Map<RedisValue>(value);
        }
    }
}