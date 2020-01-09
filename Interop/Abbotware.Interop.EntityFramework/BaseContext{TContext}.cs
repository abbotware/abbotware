// -----------------------------------------------------------------------
// <copyright file="BaseContext{TContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.EntityFramework
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using Abbotware.Core;
    using Abbotware.Core.Diagnostics;
    using Abbotware.Core.Logging;
    using Abbotware.Data.Configuration;
    using Abbotware.Data.Schema;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     Base class for entity framework context that contains logic to validate the entites and the database schema
    /// </summary>
    /// <typeparam name="TContext">type of parent class</typeparam>
    public abstract class BaseContext<TContext> : DbContext
        where TContext : BaseContext<TContext>
    {
        /// <summary>
        ///     flag to indicate whether or not the entity classes in the context have been validated against the database schema
        /// </summary>
        private static volatile bool schemaAlreadyChecked;

        // private bool isInMemory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseContext{TContext}" /> class.
        /// </summary>
        /// <param name="options">injected sql connection options</param>
        /// <param name="adapter">injected adapter</param>
        /// <param name="logger">injected logger</param>
        protected BaseContext(ISqlConnectionOptions options, IDbContextOptionsAdapter<TContext> adapter, ILogger logger)
            : base(Arguments.EnsureNotNull(adapter, nameof(adapter)).Convert(options))
        {
            Arguments.NotNull(options, nameof(options));
            Arguments.NotNull(logger, nameof(logger));

            this.SqlConnectionConfiguration = options;
            this.Logger = logger;

            this.CheckSchemaAgainstEntityModels();

            // TODO:
            //// Database.SetInitializer<TContext>(null);

            // This only works if CodeMigrations are used
            //// this.Database.CompatibleWithModel(true);

            //// Database.SetInitializer(new MigrateDatabaseToLatestVersion<TContext, SchemaValidationConfiguration<TContext>>());

            // TODO:
            //// this.Database.CommandTimeout = (int)this.SqlConnectionConfiguration.CommandTimeout.TotalSeconds;
        }

        /// <summary>
        ///     Gets the current sql connection configuration
        /// </summary>
        public ISqlConnectionOptions SqlConnectionConfiguration { get; }

        /// <summary>
        ///     Gets the injected logger for the class
        /// </summary>
        protected ILogger Logger { get; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ////modelBuilder.Entity()
            ////    .Properties<string>()
            ////    .Configure(c => c.IsUnicode(false));

            ////modelBuilder.Properties<decimal>()
            ////    .Configure(c => c.HasPrecision(22, 8));

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        ///     Validates the entites against the database schema
        /// </summary>
        private void CheckSchemaAgainstEntityModels()
        {
            if (!this.SqlConnectionConfiguration.ValidateSchema)
            {
                return;
            }

            if (!this.SqlConnectionConfiguration.SupportsMetadata)
            {
                return;
            }

            if (schemaAlreadyChecked)
            {
                return;
            }

            using var databaseMetadata = new DatabaseMetadata(this.SqlConnectionConfiguration, this.Logger);

            var dataSets = this.GetType()
                .GetProperties()
                .Where(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .ToList();

            foreach (var dataSet in dataSets)
            {
                var entityType = dataSet.PropertyType.GetGenericArguments().Single();

                var tableSchema = "dbo";
                var tableName = entityType.Name;

                if (dataSet.PropertyType.BaseType == typeof(object))
                {
                    this.Logger.Debug("Skipping DBSet:{0} type:{1} - inhertince validation not supported", dataSet.Name, dataSet.PropertyType.FullName);
                    continue;
                }

                // Get Schema / Table
                var tableAttribute = ReflectionHelper.SingleOrDefaultAttribute<TableAttribute>(entityType);

                if (tableAttribute != null)
                {
                    if (!string.IsNullOrWhiteSpace(tableAttribute.Schema))
                    {
                        tableSchema = tableAttribute.Schema;
                    }

                    tableName = tableAttribute.Name;
                }

                var properties = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                var tableMeta = databaseMetadata.Table(tableSchema, tableName);

                if (tableMeta == null)
                {
                    var message = FormattableString.Invariant($"Schema.Table : {tableSchema}.{tableName} not found in sql meta data for dbset:{dataSet.Name}");
                    throw new DataException(message);
                }

                foreach (var property in properties)
                {
                    if (!SqlTypeMapping.SupportedCSharpType(property))
                    {
                        this.Logger.Debug("Skipping Property:{0} type:{1}", property.Name, property.PropertyType.FullName);
                        continue;
                    }

                    var columnName = property.Name;

                    var columnAttribute = ReflectionHelper.SingleOrDefaultAttribute<ColumnAttribute>(property);

                    if (!string.IsNullOrWhiteSpace(columnAttribute?.Name))
                    {
                        columnName = columnAttribute.Name;
                    }

                    var columnMeta = tableMeta.Column(columnName);

                    if (columnMeta == null)
                    {
                        var message = FormattableString.Invariant($"Table.Column={tableName}.{columnName} not found for Class.Property={entityType.Name}.{property.Name} type:{property.PropertyType.Name}");
                        throw new DataException(message);
                    }

                    // check if Type matches / compatable
                    if (!SqlTypeMapping.AreTypesCompatible(columnMeta, property))
                    {
                        var message = FormattableString.Invariant($"incompatable data type Class.Property={entityType.Name}.{property.Name} type:{property.PropertyType.Name} Table.Column={tableName}.{columnName} type:{columnMeta.SqlDbType} length:{columnMeta.MaxLength}");
                        throw new DataException(message);
                    }
                }
            }

            schemaAlreadyChecked = true;
        }
    }
}