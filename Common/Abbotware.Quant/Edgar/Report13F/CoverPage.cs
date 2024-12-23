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
    [property:Key]
    [Name("ACCESSION_NUMBER")]
    string AccessionNumber,
    [Name("REPORTCALENDARORQUARTER")]
    DateOnly ReportCalendarOrQuarter,
    [Name("ISAMENDMENT")]
    [TypeConverter(typeof(YesNoBooleanConverter))]
    bool IsAmendment,
    [Name("AMENDMENTNO")]
    int? AmendmentNumber,
    [Name("AMENDMENTTYPE")]
    [TypeConverter(typeof(NullableEnumMemberConverter<AmendmentType>))]
    AmendmentType? AmendmentType,
    [Name("CONFDENIEDEXPIRED")]
    [TypeConverter(typeof(YesNoBooleanConverter))]
    bool? ConfDeniedExpired,
    [Name("DATEDENIEDEXPIRED")]
    DateOnly? DateDeniedExpired,
    [Name("DATEREPORTED")]
    DateOnly? DateReported,
    [Name("REASONFORNONCONFIDENTIALITY")]
    string? ReasonForNonconfidentiality,
    [Name("FILINGMANAGER_NAME")]
    string FilingManagerName,
    [Name("FILINGMANAGER_STREET1")]
    string FilingManagerStreet1,
    [Name("FILINGMANAGER_STREET2")]
    string FilingManagerStreet2,
    [Name("FILINGMANAGER_CITY")]
    string FilingManagerCity,
    [Name("FILINGMANAGER_STATEORCOUNTRY")]
    string FilingManagerStateOrCountry,
    [Name("FILINGMANAGER_ZIPCODE")]
    string FilingManagerZipCode,
    [Name("REPORTTYPE")]
    [TypeConverter(typeof(EnumMemberConverter<ReportType>))]
    ReportType ReportType,
    [Name("FORM13FFILENUMBER")]
    string? Form13FFileNumber,
    [Name("CRDNUMBER")]
    string? CrdNumber,
    [Name("SECFILENUMBER")]
    string? SecFileNumber,
    [Name("PROVIDEINFOFORINSTRUCTION5")]
    [TypeConverter(typeof(YesNoBooleanConverter))]
    bool ProvideInfoForInstruction5,
    [Name("ADDITIONALINFORMATION")]
    string? AdditionalInformation);