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
    [param:Name("ACCESSION_NUMBER")]
    [property:Name("ACCESSION_NUMBER")]
    string AccessionNumber,
    [property:Key]
    [param:Name("INFOTABLE_SK")]
    [property:Name("INFOTABLE_SK")]
    long InfoTableKey,
    [param:Name("NAMEOFISSUER")]
    [property:Name("NAMEOFISSUER")]
    string NameOfIssuer,
    [param:Name("TITLEOFCLASS")]
    [property:Name("TITLEOFCLASS")]
    string TitleOfClass,
    [param:Name("CUSIP")]
    [property:Name("CUSIP")]
    string? Cusip,
    [param:Name("FIGI")]
    [property:Name("FIGI")]
    string? Figi,
    [param:Name("VALUE")]
    [property:Name("VALUE")]
    decimal? Value,
    [param:Name("SSHPRNAMT")]
    [property:Name("SSHPRNAMT")]
    decimal Amount,
    [param:Name("SSHPRNAMTTYPE")]
    [property:Name("SSHPRNAMTTYPE")]
    [param:TypeConverter(typeof(EnumMemberConverter<AmountType>))]
    [property:TypeConverter(typeof(EnumMemberConverter<AmountType>))]
    AmountType AmountType,
    [param:Name("PUTCALL")]
    [property:Name("PUTCALL")]
    [param:TypeConverter(typeof(NullableEnumMemberConverter<PutCallType>))]
    [property:TypeConverter(typeof(NullableEnumMemberConverter<PutCallType>))]
    PutCallType? PutCallType,
    [param:Name("INVESTMENTDISCRETION")]
    [property:Name("INVESTMENTDISCRETION")]
    string? InvestmentDiscretion,
    [param:Name("OTHERMANAGER")]
    [property:Name("OTHERMANAGER")]
    string? OtherManager,
    [param:Name("VOTING_AUTH_SOLE")]
    [property:Name("VOTING_AUTH_SOLE")]
    long? VotingAuthoritySole,
    [param:Name("VOTING_AUTH_SHARED")]
    [property:Name("VOTING_AUTH_SHARED")]
    long? VotingAuthorityShared,
    [param:Name("VOTING_AUTH_NONE")]
    [property:Name("VOTING_AUTH_NONE")]
    long? VotingAuthorityNone)
{
    /// <summary>
    /// Gets the file name
    /// </summary>
    public const string FileName = "INFOTABLE.TSV";
}
