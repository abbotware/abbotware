// -----------------------------------------------------------------------
// <copyright file="FixedIncomeKeyValue.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Serialization
{
    using System.Collections.Generic;
    using Abbotware.Interop.EodHistoricalData.Models;
    using Abbotware.Interop.Newtonsoft.Plugins;

    /// <summary>
    /// convert KeyValuePair to FixedIncome
    /// </summary>
    public class FixedIncomeKeyValue : KeyValueConverter<string, FixedIncome>
    {
        /// <inheritdoc/>
        public override FixedIncome Convert(KeyValuePair<string, FixedIncome> kvp)
        {
            return kvp.Value with { Type = kvp.Key };
        }
    }
}
