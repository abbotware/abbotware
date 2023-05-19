// -----------------------------------------------------------------------
// <copyright file="MfSectorWeightKeyValue.cs" company="Abbotware, LLC">
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
    public class MfSectorWeightKeyValue : KeyListConverter<MfSectorWeight>
    {
        /// <inheritdoc/>
        public override IReadOnlyCollection<MfSectorWeight> Convert(Dictionary<string, Dictionary<string, MfSectorWeight>> values)
        {
            values = Arguments.EnsureNotNull(values, nameof(values));

            var l = new List<MfSectorWeight>();

            foreach (var outer in values)
            {
                foreach (var k in outer.Value)
                {
                    l.Add(k.Value with { Category = outer.Key });
                }
            }

            return l;
        }
    }
}
