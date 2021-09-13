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
    /// convert KeyValuePair to Holding
    /// </summary>
    public class HoldingKeyValue : KeyValueConverter<string, Holding>
    {
        /// <inheritdoc/>
        public override Holding Convert(KeyValuePair<string, Holding> kvp)
        {
            Debug.Assert(kvp.Key == kvp.Value.Name, "Key does not match property value");

            return kvp.Value;
        }
    }
}
