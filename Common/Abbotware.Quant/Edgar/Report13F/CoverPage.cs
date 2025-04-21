namespace Abbotware.Quant.Edgar.Report13F;

using System;
using System.ComponentModel.DataAnnotations;
using Abbotware.Interop.CsvHelper.Plugins;
using CsvHelper.Configuration.Attributes;

/// <summary>
/// Table / File Definition for COVERPAGE.tsv
///
/// COVERPAGE provides details of the cover page with each row having ACCESSION_NUMBER as the key.
/// </summary>
/// <param name="AccessionNumber">Unique identifier assigned by the SEC to each EDGAR submission.</param>
/// <param name="ReportCalendarOrQuarter">Report for the calendar year of quarter ended in (DD-MON-YYYY) format.</param>
/// <param name="IsAmendment">Check here if amendment</param>
/// <param name="AmendmentNumber">Amendment number</param>
/// <param name="AmendmentType">Amendment type is a restatement or adds new holdings entries.</param>
/// <param name="ConfDeniedExpired">Whether confidential treatment request is denied or expired.</param>
/// <param name="DateDeniedExpired">Pursuant to a request date when confidential treatment expired or denied in (DD-MON-YYYY) format.</param>
/// <param name="DateReported">Date securities holdings reported pursuant to a request for confidential treatment in (DD-MON-YYYY) format.</param>
/// <param name="ReasonForNonconfidentiality">Reason for non-confidentiality: denied or confidential treatment expired.</param>
/// <param name="FilingManagerName">Filing manager name.</param>
/// <param name="FilingManagerStreet1">Filing manager address street name - first line.</param>
/// <param name="FilingManagerStreet2">Filing manager address street name - second line./param>
/// <param name="FilingManagerCity">Filing manager primary city.</param>
/// <param name="FilingManagerStateOrCountry">Filing manager state or country name.</param>
/// <param name="FilingManagerZipCode">Filing manager zip code.</param>
/// <param name="ReportType">Report type: 13F holdings report; 13F notice; 13F combination report.</param>
/// <param name="Form13FFileNumber">FORM 13F file number.</param>
/// <param name="CrdNumber">CRD Number (if applicable)</param>
/// <param name="SecFileNumber">SEC File Number (if applicable)</param>
/// <param name="ProvideInfoForInstruction5">Do you wish to provide information pursuant to instruction 5?</param>
/// <param name="AdditionalInformation">Additional information.</param>
public record CoverPage(
    [property: Key]
    [param: Name("ACCESSION_NUMBER")]
    [property: Name("ACCESSION_NUMBER")]
    string AccessionNumber,

    [param: Name("REPORTCALENDARORQUARTER")]
    [property: Name("REPORTCALENDARORQUARTER")]
    DateOnly ReportCalendarOrQuarter,

    [param: Name("ISAMENDMENT")]
    [property: Name("ISAMENDMENT")]
    [param: TypeConverter(typeof(YesNoBooleanConverter))]
    [property: TypeConverter(typeof(YesNoBooleanConverter))]
    bool IsAmendment,

    [param: Name("AMENDMENTNO")]
    [property: Name("AMENDMENTNO")]
    int? AmendmentNumber,

    [param: Name("AMENDMENTTYPE")]
    [property: Name("AMENDMENTTYPE")]
    [param: TypeConverter(typeof(NullableEnumMemberConverter<AmendmentType>))]
    [property: TypeConverter(typeof(NullableEnumMemberConverter<AmendmentType>))]
    AmendmentType? AmendmentType,

    [param: Name("CONFDENIEDEXPIRED")]
    [property: Name("CONFDENIEDEXPIRED")]
    [param: TypeConverter(typeof(YesNoBooleanConverter))]
    [property: TypeConverter(typeof(YesNoBooleanConverter))]
    bool? ConfDeniedExpired,

    [param: Name("DATEDENIEDEXPIRED")]
    [property: Name("DATEDENIEDEXPIRED")]
    DateOnly? DateDeniedExpired,

    [param: Name("DATEREPORTED")]
    [property: Name("DATEREPORTED")]
    DateOnly? DateReported,

    [param: Name("REASONFORNONCONFIDENTIALITY")]
    [property: Name("REASONFORNONCONFIDENTIALITY")]
    string? ReasonForNonconfidentiality,

    [param: Name("FILINGMANAGER_NAME")]
    [property: Name("FILINGMANAGER_NAME")]
    string FilingManagerName,

    [param: Name("FILINGMANAGER_STREET1")]
    [property: Name("FILINGMANAGER_STREET1")]
    string FilingManagerStreet1,

    [param: Name("FILINGMANAGER_STREET2")]
    [property: Name("FILINGMANAGER_STREET2")]
    string FilingManagerStreet2,

    [param: Name("FILINGMANAGER_CITY")]
    [property: Name("FILINGMANAGER_CITY")]
    string FilingManagerCity,

    [param: Name("FILINGMANAGER_STATEORCOUNTRY")]
    [property: Name("FILINGMANAGER_STATEORCOUNTRY")]
    string FilingManagerStateOrCountry,

    [param: Name("FILINGMANAGER_ZIPCODE")]
    [property: Name("FILINGMANAGER_ZIPCODE")]
    string FilingManagerZipCode,

    [param: Name("REPORTTYPE")]
    [property: Name("REPORTTYPE")]
    [param: TypeConverter(typeof(EnumMemberConverter<ReportType>))]
    [property: TypeConverter(typeof(EnumMemberConverter<ReportType>))]
    ReportType ReportType,

    [param: Name("FORM13FFILENUMBER")]
    [property: Name("FORM13FFILENUMBER")]
    string? Form13FFileNumber,

    [param: Name("CRDNUMBER")]
    [property: Name("CRDNUMBER")]
    string? CrdNumber,

    [param: Name("SECFILENUMBER")]
    [property: Name("SECFILENUMBER")]
    string? SecFileNumber,

    [param: Name("PROVIDEINFOFORINSTRUCTION5")]
    [property: Name("PROVIDEINFOFORINSTRUCTION5")]
    [param: TypeConverter(typeof(YesNoBooleanConverter))]
    [property: TypeConverter(typeof(YesNoBooleanConverter))]
    bool ProvideInfoForInstruction5,

    [param: Name("ADDITIONALINFORMATION")]
    [property: Name("ADDITIONALINFORMATION")]
    string? AdditionalInformation)
{
    /// <summary>
    /// Gets the file name
    /// </summary>
    public const string FileName = "COVERPAGE.TSV";
}