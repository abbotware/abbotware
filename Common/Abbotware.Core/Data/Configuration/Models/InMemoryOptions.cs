// -----------------------------------------------------------------------
// <copyright file="InMemoryOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Data.Configuration.Models
{
    using System;

    /// <summary>
    /// POCO class for In Memory database options
    /// </summary>
    public class InMemoryOptions : IInMemoryOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryOptions"/> class.
        /// </summary>
        public InMemoryOptions()
            : this(Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryOptions"/> class.
        /// </summary>
        /// <param name="databaseId">database id</param>
        public InMemoryOptions(string databaseId)
        {
            this.DatabaseId = databaseId;
        }

        /// <summary>
        /// Gets the database Id
        /// </summary>
        public string DatabaseId { get; }
    }
}