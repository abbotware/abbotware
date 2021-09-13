// -----------------------------------------------------------------------
// <copyright file="WorldRegionKeyValue.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Serialization
{
    using System.Collections.Generic;
    using Abbotware.Interop.EodHistoricalData.Models;
    using Abbotware.Interop.Newtonsoft.Plugins;

    /// <summary>
    /// convert KeyValuePair to WorldRegion
    /// </summary>
    public class WorldRegionKeyValue : KeyValueConverter<string, WorldRegion>
    {
        /// <inheritdoc/>
        public override WorldRegion Convert(KeyValuePair<string, WorldRegion> kvp)
        {
            return kvp.Value with { Type = kvp.Key };
        }
    }
}
