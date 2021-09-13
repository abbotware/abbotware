// -----------------------------------------------------------------------
// <copyright file="SectorWeightKeyValue.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Serialization
{
    using System.Collections.Generic;
    using Abbotware.Interop.EodHistoricalData.Models;
    using Abbotware.Interop.Newtonsoft.Plugins;

    /// <summary>
    /// convert KeyValuePair to SectorWeight
    /// </summary>
    public class SectorWeightKeyValue : KeyValueConverter<string, SectorWeight>
    {
        /// <inheritdoc/>
        public override SectorWeight Convert(KeyValuePair<string, SectorWeight> kvp)
        {
            return kvp.Value with { Type = kvp.Key };
        }
    }
}
