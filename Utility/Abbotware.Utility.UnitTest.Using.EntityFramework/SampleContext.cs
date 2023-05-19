// -----------------------------------------------------------------------
// <copyright file="SampleContext.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Utility.UnitTest.Using.EntityFramework
{
    using System.Collections.Generic;
    using System.Globalization;
    using Abbotware.Core;
    using Abbotware.Utility.UnitTest.Using.EntityFramework.Models;
    using Microsoft.EntityFrameworkCore;

    public class SampleContext : DbContext
    {
        public SampleContext(DbContextOptions<SampleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ModelIntKey> Bas => this.Set<ModelIntKey>();

        public virtual DbSet<ModelIntKey> ModelIntKeys => this.Set<ModelIntKey>();

        public virtual DbSet<ModelEnumKey> ModelEnumKeys => this.Set<ModelEnumKey>();

        public virtual DbSet<ModelGuidKey> ModelGuidKeys => this.Set<ModelGuidKey>();

        public virtual DbSet<ModelStringKey> ModelStringKeys => this.Set<ModelStringKey>();

        public virtual DbSet<CompositeStringKey> CompositeStringKeys => this.Set<CompositeStringKey>();

        public static ICollection<ModelIntKey> CreateData()
        {
            var l = new List<ModelIntKey>();

            for (int i = 0; i < 10; ++i)
            {
                l.Add(new ModelIntKey { Id = i, Name = i.ToString(CultureInfo.InvariantCulture) });
            }

            return l;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = Arguments.EnsureNotNull(modelBuilder, nameof(modelBuilder));

            modelBuilder.Entity<CompositeStringKey>()
                .HasKey(c => new { c.Id, c.Name });
        }
    }
}
