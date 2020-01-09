// -----------------------------------------------------------------------
// <copyright file="AddAbbotwareContext{TContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Using.Castle.Fluent.Implementation
{
    using Abbotware.Data.Configuration;
    using Abbotware.Interop.EntityFramework;
    using Abbotware.Using.Castle;
    using Abbotware.Using.Castle.Fluent;
    using global::Castle.Windsor;

    /// <summary>
    /// Fluent API implementation for IAddAbbotwareContext
    /// </summary>
    /// <typeparam name="TContext">context type</typeparam>
    public class AddAbbotwareContext<TContext> : AddDbContext<TContext>, IAddAbbotwareContext<TContext>
        where TContext : BaseContext<TContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddAbbotwareContext{TContext}"/> class.
        /// </summary>
        /// <param name="container">container to configure</param>
        public AddAbbotwareContext(IWindsorContainer container)
            : base(container)
        {
        }

        /// <inheritdoc/>
        public override IConnectionStringSource<TContext> ConnectionStringName(string name)
        {
            return new AbbotwareConnectionStringSource<TContext>(this.Container, name);
        }

        /// <inheritdoc/>
        protected override void OnRegister(ISqlConnectionOptions configuration, IDbContextOptionsAdapter<TContext> adapter)
        {
            this.Container.RegisterAbbotwareContext<TContext>(configuration, adapter);
        }
    }
}
