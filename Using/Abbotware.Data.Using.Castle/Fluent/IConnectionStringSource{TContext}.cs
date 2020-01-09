// -----------------------------------------------------------------------
// <copyright file="IConnectionStringSource{TContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Using.Castle.Fluent
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Fluent interface for locating the connection string
    /// </summary>
    /// <typeparam name="TContext">context type</typeparam>
    public interface IConnectionStringSource<TContext>
        where TContext : DbContext
    {
        /// <summary>
        /// Use Microsoft configuration extensions to locate the connection string
        /// </summary>
        /// <param name="configuration">configuration interface</param>
        void UseMicrosoft(IConfiguration configuration);

        /// <summary>
        /// Use the app config locate the connection string
        /// </summary>
        void UseAppConfig();
    }
}
