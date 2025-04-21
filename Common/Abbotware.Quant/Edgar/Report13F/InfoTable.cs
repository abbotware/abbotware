// -----------------------------------------------------------------------
// <copyright file="InfoTable.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Edgar.Report13F;

using System.ComponentModel.DataAnnotations;
using Abbotware.Interop.CsvHelper.Plugins;
using CsvHelper.Configuration.Attributes;

/// <summary>
/// Table / File Definition for INFOTABLE.tsv
/// 
/// INFOTABLE data contains the information table with each row having ACCESSION_NUMBER and INFOTABLE_SK as the primary keys.
/// </summary>
/// <param name="AccessionNumber">Unique identifier assigned by the SEC to each EDGAR submission.</param>
/// <param name="InfoTableKey">Info table surrogate key.</param>
/// <param name="NameOfIssuer">Name of issuer.</param>
/// <param name="TitleOfClass">Title of class.</param>
/// <param name="Cusip">CUSIP number of security.</param>
/// <param name="Figi">Financial Instrument Global Identifier (FIGI)</param>
/// <param name="Value">Market value. Starting on January 3, 2023, market value is reported rounded to the nearest dollar.  Previously, market value was reported in thousands.</param>
/// <param name="Amount">Number of shares or principal amount.</param>
/// <param name="AmountType">Shares (SH) or principal amount (PRN) type.</param>
/// <param name="PutCallType">PUT/CALL.</param>
/// <param name="InvestmentDiscretion">Investment discretion.</param>
/// <param name="OtherManager">Sequence number of other manager included in report with whom discretion is shared.</param>
/// <param name="VotingAuthoritySole">Voting authority sole.</param>
/// <param name="VotingAuthorityShared">Voting authority shared.</param>
/// <param name="VotingAuthorityNone">Voting authority none.</param>
public record class InfoTable(
    [property:Key]
    [param:Name(InfoTable.Fields.AccessionNumber)]
    [property:Name(InfoTable.Fields.AccessionNumber)]
    string AccessionNumber,
    [property:Key]
    [param:Name(InfoTable.Fields.InfoTableKey)]
    [property:Name(InfoTable.Fields.InfoTableKey)]
    long InfoTableKey,
    [param:Name(InfoTable.Fields.NameOfIssuer)]
    [property:Name(InfoTable.Fields.NameOfIssuer)]
    string NameOfIssuer,
    [param:Name(InfoTable.Fields.TitleOfClass)]
    [property:Name(InfoTable.Fields.TitleOfClass)]
    string TitleOfClass,
    [param:Name(InfoTable.Fields.Cusip)]
    [property:Name(InfoTable.Fields.Cusip)]
    string? Cusip,
    [param:Name(InfoTable.Fields.Figi)]
    [property:Name(InfoTable.Fields.Figi)]
    string? Figi,
    [param:Name(InfoTable.Fields.Value)]
    [property:Name(InfoTable.Fields.Value)]
    decimal? Value,
    [param:Name(InfoTable.Fields.Amount)]
    [property:Name(InfoTable.Fields.Amount)]
    decimal Amount,
    [param:Name(InfoTable.Fields.AmountType)]
    [property:Name(InfoTable.Fields.AmountType)]
    [param:TypeConverter(typeof(EnumMemberConverter<AmountType>))]
    [property:TypeConverter(typeof(EnumMemberConverter<AmountType>))]
    AmountType AmountType,
    [param:Name(InfoTable.Fields.PutCallType)]
    [property:Name(InfoTable.Fields.PutCallType)]
    [param:TypeConverter(typeof(NullableEnumMemberConverter<PutCallType>))]
    [property:TypeConverter(typeof(NullableEnumMemberConverter<PutCallType>))]
    PutCallType? PutCallType,
    [param:Name(InfoTable.Fields.InvestmentDiscretion)]
    [property:Name(InfoTable.Fields.InvestmentDiscretion)]
    string? InvestmentDiscretion,
    [param:Name(InfoTable.Fields.OtherManager)]
    [property:Name(InfoTable.Fields.OtherManager)]
    string? OtherManager,
    [param:Name(InfoTable.Fields.VotingAuthoritySole)]
    [property:Name(InfoTable.Fields.VotingAuthoritySole)]
    long? VotingAuthoritySole,
    [param:Name(InfoTable.Fields.VotingAuthorityShared)]
    [property:Name(InfoTable.Fields.VotingAuthorityShared)]
    long? VotingAuthorityShared,
    [param:Name(InfoTable.Fields.VotingAuthorityNone)]
    [property:Name(InfoTable.Fields.VotingAuthorityNone)]
    long? VotingAuthorityNone)
{
    /// <summary>
    /// Gets the file name
    /// </summary>
    public const string FileName = "INFOTABLE.TSV";

    /// <summary>
    /// Names of the fields
    /// </summary>
    public static class Fields {
        public const string AccessionNumber = "ACCESSION_NUMBER";
        public const string InfoTableKey = "INFOTABLE_SK";
        public const string NameOfIssuer = "NAMEOFISSUER";
        public const string TitleOfClass = "TITLEOFCLASS";
        public const string Cusip = "CUSIP";
        public const string Figi = "FIGI";
        public const string Value = "VALUE";
        public const string Amount = "SSHPRNAMT";
        public const string AmountType = "SSHPRNAMTTYPE";
        public const string PutCallType = "PUTCALL";
        public const string InvestmentDiscretion = "INVESTMENTDISCRETION";
        public const string OtherManager = "OTHERMANAGER";
        public const string VotingAuthoritySole = "VOTING_AUTH_SOLE";
        public const string VotingAuthorityShared = "VOTING_AUTH_SHARED";
        public const string VotingAuthorityNone = "VOTING_AUTH_NONE";
    }
}
