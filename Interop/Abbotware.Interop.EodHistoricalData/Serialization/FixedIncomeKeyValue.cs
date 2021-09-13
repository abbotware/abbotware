// -----------------------------------------------------------------------
// <copyright file="HoldingKeyValue.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Serialization
{
    using System.Collections.Generic;
    using System.Diagnostics;
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
            Debug.Assert(kvp.Key == kvp.Value.Type, "Key does not match property value");

            return kvp.Value;
        }
    }
}
