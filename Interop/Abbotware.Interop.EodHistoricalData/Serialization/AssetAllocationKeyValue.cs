// -----------------------------------------------------------------------
// <copyright file="AssetAllocationKeyValue.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Serialization
{
    using System.Collections.Generic;
    using Abbotware.Interop.EodHistoricalData.Models;
    using Abbotware.Interop.Newtonsoft.Plugins;

    /// <summary>
    /// convert KeyValuePair to AssetAllocation
    /// </summary>
    public class AssetAllocationKeyValue : KeyValueConverter<string, AssetAllocation>
    {
        /// <inheritdoc/>
        public override AssetAllocation Convert(KeyValuePair<string, AssetAllocation> kvp)
        {
            return kvp.Value with { Type = kvp.Key };
        }
    }
}
