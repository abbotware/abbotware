// -----------------------------------------------------------------------
// <copyright file="ModelStringKey.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Utility.UnitTest.Using.EntityFramework.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ModelStringKey
    {
        [Key]
        [MaxLength(50)]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}