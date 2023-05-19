// -----------------------------------------------------------------------
// <copyright file="CastleExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
namespace Abbotware.Using.Castle
{
    using Abbotware.Core;
    using Abbotware.Data.Configuration;
    using Abbotware.Interop.EntityFramework;
    using Abbotware.Using.Castle.Fluent;
    using Abbotware.Using.Castle.Fluent.Implementation;
    using global::Castle.Facilities.TypedFactory;
    using global::Castle.MicroKernel.Registration;
    using global::Castle.Windsor;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Extension methods for castle registraion
    /// </summary>
    public static class CastleExtensions
    {
        /// <summary>
        /// Adds and configures entity framework features
        /// </summary>
        /// <param name="container">container</param>
        public static void AddEntityFrameworkSupport(this IWindsorContainer container)
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            container.Register(Component.For<IDbContextFactory>().AsFactory());
        }

        /// <summary>
        /// Adds and configures a DbContext to the configure
        /// </summary>
        /// <typeparam name="TContext">context type</typeparam>
        /// <param name="container">container</param>
        /// <returns>Add Context</returns>
        public static IAddDbContext<TContext> AddDbContext<TContext>(this IWindsorContainer container)
            where TContext : DbContext
        {
            Arguments.NotNull(container, nameof(container));

            return new AddDbContext<TContext>(container);
        }

        /// <summary>
        /// Registers an Abbotware BaseContext
        /// </summary>
        /// <typeparam name="TContext">context type</typeparam>
        /// <param name="container">container</param>
        /// <param name="options">connection configuration</param>
        /// <param name="adapter">options adapter</param>
        public static void RegisterDbContext<TContext>(this IWindsorContainer container, ISqlConnectionOptions options, IDbContextOptionsAdapter<TContext> adapter)
            where TContext : DbContext
        {
            container = Arguments.EnsureNotNull(container, nameof(container));
            adapter = Arguments.EnsureNotNull(adapter, nameof(adapter));

            Arguments.NotNull(options, nameof(options));

            container.Register(Component.For<TContext>()
                .ImplementedBy<TContext>()
                .LifestyleTransient()
                .DependsOn(Dependency.OnValue<DbContextOptions<TContext>>(adapter.Convert(options))));
        }

        /// <summary>
        /// Adds and configures an Abbotware BaseContext
        /// </summary>
        /// <typeparam name="TContext">context type</typeparam>
        /// <param name="container">container</param>
        /// <returns>Add Context</returns>
        public static IAddDbContext<TContext> AddAbbotwareContext<TContext>(this IWindsorContainer container)
          where TContext : BaseContext<TContext>
        {
            Arguments.NotNull(container, nameof(container));

            return new AddAbbotwareContext<TContext>(container);
        }

        /// <summary>
        /// Registers an Abbotware BaseContext
        /// </summary>
        /// <typeparam name="TContext">context type</typeparam>
        /// <param name="container">container</param>
        /// <param name="connection">connection configuration</param>
        /// <param name="adapter">options adapter</param>
        public static void RegisterAbbotwareContext<TContext>(this IWindsorContainer container, ISqlConnectionOptions connection, IDbContextOptionsAdapter<TContext> adapter)
            where TContext : BaseContext<TContext>
        {
            container = Arguments.EnsureNotNull(container, nameof(container));
            connection = Arguments.EnsureNotNull(connection, nameof(connection));

            container.Register(Component.For<TContext>()
                .ImplementedBy<TContext>()
                .LifestyleTransient()
                .DependsOn(Dependency.OnValue<ISqlConnectionOptions>(connection))
                .DependsOn(Dependency.OnValue<IDbContextOptionsAdapter<TContext>>(adapter)));
        }
    }
}
