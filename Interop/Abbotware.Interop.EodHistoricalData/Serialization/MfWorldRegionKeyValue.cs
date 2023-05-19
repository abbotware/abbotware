// -----------------------------------------------------------------------
// <copyright file="MfWorldRegionKeyValue.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Serialization
{
    using System.Collections.Generic;
    using Abbotware.Core;
    using Abbotware.Interop.EodHistoricalData.Models;
    using Abbotware.Interop.Newtonsoft.Plugins;

    /// <summary>
    /// convert KeyValuePair to MfSectorWeight
    /// </summary>
    public class MfWorldRegionKeyValue : KeyListConverter<MfWorldRegion>
    {
        /// <inheritdoc/>
        public override IReadOnlyCollection<MfWorldRegion> Convert(Dictionary<string, Dictionary<string, MfWorldRegion>> values)
        {
            values = Arguments.EnsureNotNull(values, nameof(values));

            var l = new List<MfWorldRegion>();

            foreach (var outer in values)
            {
                foreach (var k in outer.Value)
                {
                    l.Add(k.Value with { Region = outer.Key });
                }
            }

            return l;
        }
    }
}
