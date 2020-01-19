// -----------------------------------------------------------------------
// <copyright file="SampleContext.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Utility.UnitTest.Using.EntityFramework
{
    using System.Collections.Generic;
    using System.Globalization;
    using Abbotware.Utility.UnitTest.Using.EntityFramework.Models;
    using Microsoft.EntityFrameworkCore;

    public class SampleContext : DbContext
    {
        public SampleContext(DbContextOptions<SampleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ModelIntKey> Bas { get; set; }

        public virtual DbSet<ModelIntKey> ModelIntKeys { get; set; }

        public virtual DbSet<ModelEnumKey> ModelEnumKeys { get; set; }

        public virtual DbSet<ModelGuidKey> ModelGuidKeys { get; set; }

        public virtual DbSet<ModelStringKey> ModelStringKeys { get; set; }

        public virtual DbSet<CompositeStringKey> CompositeStringKeys { get; set; }

        public static List<ModelIntKey> CreateData()
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
#pragma warning disable CA1062 // Validate arguments of public methods
            modelBuilder.Entity<CompositeStringKey>()
#pragma warning restore CA1062 // Validate arguments of public methods
                .HasKey(c => new { c.Id, c.Name });
        }
    }
}
