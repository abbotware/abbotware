// -----------------------------------------------------------------------
// <copyright file="ModelGuidKey.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Utility.UnitTest.Using.EntityFramework.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ModelGuidKey
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}