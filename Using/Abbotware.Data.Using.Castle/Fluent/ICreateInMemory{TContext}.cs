// -----------------------------------------------------------------------
// <copyright file="ICreateInMemory{TContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Using.Castle.Fluent
{
    using System;
    using Abbotware.Interop.EntityFramework;

    /// <summary>
    /// Flue interface for creating an InMemory database (after registration)
    /// </summary>
    /// <typeparam name="TContext">context type</typeparam>
    public interface ICreateInMemory<TContext>
    {
        /// <summary>
        /// Creates the in memory database
        /// </summary>
        /// <param name="action">action to run to build</param>
        void Create(Action<IDbContextFactory> action);
    }
}