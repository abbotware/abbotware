// -----------------------------------------------------------------------
// <copyright file="ModelEnumKey.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Utility.UnitTest.Using.EntityFramework.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ModelEnumKey
    {
        [Key]
        public EnumTypeId Id { get; set; }

        public string Name { get; set; }
    }
}