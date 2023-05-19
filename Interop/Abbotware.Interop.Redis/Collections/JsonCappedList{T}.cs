// -----------------------------------------------------------------------
// <copyright file="JsonCappedList{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Redis.Collections
{
    using Abbotware.Interop.Newtonsoft.Plugins;
    using StackExchange.Redis;

    /// <summary>
    /// Redis List using Json serialization
    /// </summary>
    /// <typeparam name="T">List Element Type</typeparam>
    public class JsonCappedList<T> : SerializedCappedList<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonCappedList{T}"/> class.
        /// </summary>
        /// <param name="remotekey">remote key name</param>
        /// <param name="capacity">capacity of the list</param>
        /// <param name="database">redis database</param>
        public JsonCappedList(string remotekey, int capacity, IDatabase database)
            : base(remotekey, capacity, database, new NewtonsoftJsonSerializer())
        {
        }
    }
}