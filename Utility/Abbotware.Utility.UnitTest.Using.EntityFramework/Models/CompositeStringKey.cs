// -----------------------------------------------------------------------
// <copyright file="CompositeStringKey.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Utility.UnitTest.Using.EntityFramework.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CompositeStringKey
    {
        [MaxLength(50)]
        [Column(Order =1)]
        public string Id { get; set; }

        [MaxLength(50)]
        [Column(Order = 1)]
        public string Name { get; set; }
    }
}