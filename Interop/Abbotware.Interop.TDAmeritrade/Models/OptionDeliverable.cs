// -----------------------------------------------------------------------
// <copyright file="OptionDeliverable.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Options Delieverable POCO
    /// </summary>
    /// <param name="Symbol">Symbol</param>
    /// <param name="Asset">Asset Type</param>
    /// <param name="DeliverableUnits">Unit Type</param>
    /// <param name="CurrencyType">Currency</param>
    public record OptionDeliverable(
        [property: Key][property: MaxLength(TDAmeritradeConstants.SymbolLength)] string Symbol,
        AssetType Asset,
        string DeliverableUnits,
        string CurrencyType);
}