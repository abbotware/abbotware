// -----------------------------------------------------------------------
// <copyright file="IAddDbContext{TContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Using.Castle.Fluent
{
    using Abbotware.Data.Configuration;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Fluent Interface for adding a Db Context
    /// </summary>
    /// <typeparam name="TContext">context type</typeparam>
    public interface IAddDbContext<TContext>
        where TContext : DbContext
    {
        /// <summary>
        /// Adds a context using In Memory database
        /// </summary>
        /// <returns>fluent api for creating the inmemory context</returns>
        ICreateInMemory<TContext> UseInMemory();

        /// <summary>
        /// Adds a context using In Memory database
        /// </summary>
        /// <param name="databaseId">database id</param>
        /// <returns>fluent api for creating the inmemory context</returns>
        ICreateInMemory<TContext> UseInMemory(string databaseId);

        /// <summary>
        /// Adds a context using In Memory database
        /// </summary>
        /// <param name="inMemory">in memory options</param>
        /// <returns>fluent api for creating the inmemory context</returns>
        ICreateInMemory<TContext> UseInMemory(IInMemoryOptions inMemory);

        /// <summary>
        /// Adds a context using a connection string name
        /// </summary>
        /// <param name="name">connection string name</param>
        /// <returns>fluent api for configuring the connection string source</returns>
        IConnectionStringSource<TContext> ConnectionStringName(string name);

        /// <summary>
        /// Adds a context using a connection string value
        /// </summary>
        /// <param name="connectionString">connection string value</param>
        void ConnectionStringValue(string connectionString);
    }
}