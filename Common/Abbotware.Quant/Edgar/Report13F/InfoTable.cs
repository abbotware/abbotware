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
    [Name("ACCESSION_NUMBER")]
    string AccessionNumber,
    [property:Key]
    [Name("INFOTABLE_SK")]
    long InfoTableKey,
    [Name("NAMEOFISSUER")]
    string NameOfIssuer,
    [Name("TITLEOFCLASS")]
    string TitleOfClass,
    [Name("CUSIP")]
    string? Cusip,
    [Name("FIGI")]
    string? Figi,
    [Name("VALUE")]
    decimal? Value,
    [Name("SSHPRNAMT")]
    decimal Amount,
    [Name("SSHPRNAMTTYPE")]
    [TypeConverter(typeof(EnumMemberConverter<AmountType>))]
    AmountType AmountType,
    [Name("PUTCALL")]
    [TypeConverter(typeof(NullableEnumMemberConverter<PutCallType>))]
    PutCallType? PutCallType,
    [Name("INVESTMENTDISCRETION")]
    string? InvestmentDiscretion,
    [Name("OTHERMANAGER")]
    string? OtherManager,
    [Name("VOTING_AUTH_SOLE")]
    long? VotingAuthoritySole,
    [Name("VOTING_AUTH_SHARED")]
    long? VotingAuthorityShared,
    [Name("VOTING_AUTH_NONE")]
    long? VotingAuthorityNone);