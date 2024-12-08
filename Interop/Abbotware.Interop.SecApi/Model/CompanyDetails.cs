// -----------------------------------------------------------------------
// <copyright file="CompanyDetails.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.SecApi.Model
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Company Details POCO
    /// </summary>
    /// <param name="Name">Exchange Name</param>
    /// <param name="Ticker">the ticker symbol of the company.</param>
    /// <param name="Cik">the CIK of the company. Trailing zeros are removed.</param>
    /// <param name="Cusip">one or multiple CUSIPs linked to the company. Multiple CUSIPs are delimited by space</param>
    /// <param name="Exchange">the main exchange the company is listed on</param>
    /// <param name="IsDelisted">true if the company is no longer listed, false otherwise.</param>
    /// <param name="Category">The security category</param>
    /// <param name="Sector">the sector of the company,</param>
    /// <param name="Industry">the industry of the company,</param>
    /// <param name="Sic"> four-digit SIC code</param>
    /// <param name="SicSector"> SIC sector name of the company</param>
    /// <param name="SicIndustry">SIC industry name of the company</param>
    /// <param name="FamaSector">Fama-French Sector</param>
    /// <param name="FamaIndustry">Fama-French Industry</param>
    /// <param name="Currency">operating currency of the company, e.g. "USD"</param>
    /// <param name="Location">location of the company's headquarters</param>
    /// <param name="Id">unique internal ID of the company</param>
    public record CompanyDetails(
        [property:JsonPropertyName("name")]
        string Name,
        [property:JsonPropertyName("ticker")]
        string Ticker,
        [property:JsonPropertyName("cik")]
        string Cik,
        [property:JsonPropertyName("cusip")]
        string Cusip,
        [property:JsonPropertyName("exchange")]
        string Exchange,
        [property:JsonPropertyName("isDelisted")]
        bool IsDelisted,
        [property:JsonPropertyName("category")]
        string Category,
        [property:JsonPropertyName("sector")]
        string Sector,
        [property:JsonPropertyName("industry")]
        string Industry,
        [property:JsonPropertyName("sic")]
        string Sic,
        [property:JsonPropertyName("sicSector")]
        string SicSector,
        [property:JsonPropertyName("sicIndustry")]
        string SicIndustry,
        [property:JsonPropertyName("famaSector")]
        string FamaSector,
        [property:JsonPropertyName("famaIndustry")]
        string FamaIndustry,
        [property:JsonPropertyName("currency")]
        string Currency,
        [property:JsonPropertyName("location")]
        string Location,
        [property:JsonPropertyName("id")]
        string Id)
    {
    }
}