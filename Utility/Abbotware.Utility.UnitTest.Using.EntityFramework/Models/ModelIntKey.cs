// -----------------------------------------------------------------------
// <copyright file="ModelIntKey.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Utility.UnitTest.Using.EntityFramework.Models
{
    public class ModelIntKey : BaseIntKey
    {
        public override SomeEnumType Setting { get; set; } = SomeEnumType.Basic;
    }
}