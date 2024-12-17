// -----------------------------------------------------------------------
// <copyright file="IAddAbbotwareContext{TContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Using.Castle.Fluent
{
    using Abbotware.Interop.EntityFramework;

    /// <summary>
    /// Fluent Interface for adding a Db Context
    /// </summary>
    /// <typeparam name="TContext">context type</typeparam>
    public interface IAddAbbotwareContext<TContext> : IAddDbContext<TContext>
        where TContext : BaseContext<TContext>
    {
    }
}