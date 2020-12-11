// -----------------------------------------------------------------------
// <copyright file="BaseIntKey.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Utility.UnitTest.Using.EntityFramework.Models
{
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseIntKey
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public abstract SomeEnumType Setting { get; set; }
    }
}