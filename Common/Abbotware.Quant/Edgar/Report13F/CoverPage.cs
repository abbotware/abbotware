// -----------------------------------------------------------------------
// <copyright file="CoverPage.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

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
/// <param name="FilingManagerStreet2">Filing manager address street name - second line.</param>
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
    [param: Name(CoverPage.Fields.AccessionNumber)]
    [property: Name(CoverPage.Fields.AccessionNumber)]
    string AccessionNumber,

    [param: Name(CoverPage.Fields.ReportCalendarOrQuarter)]
    [property: Name(CoverPage.Fields.ReportCalendarOrQuarter)]
    DateOnly ReportCalendarOrQuarter,

    [param: Name(CoverPage.Fields.IsAmendment)]
    [property: Name(CoverPage.Fields.IsAmendment)]
    [param: TypeConverter(typeof(YesNoBooleanConverter))]
    [property: TypeConverter(typeof(YesNoBooleanConverter))]
    bool IsAmendment,

    [param: Name(CoverPage.Fields.AmendmentNumber)]
    [property: Name(CoverPage.Fields.AmendmentNumber)]
    int? AmendmentNumber,

    [param: Name(CoverPage.Fields.AmendmentType)]
    [property: Name(CoverPage.Fields.AmendmentType)]
    [param: TypeConverter(typeof(NullableEnumMemberConverter<AmendmentType>))]
    [property: TypeConverter(typeof(NullableEnumMemberConverter<AmendmentType>))]
    AmendmentType? AmendmentType,

    [param: Name(CoverPage.Fields.ConfDeniedExpired)]
    [property: Name(CoverPage.Fields.ConfDeniedExpired)]
    [param: TypeConverter(typeof(YesNoBooleanConverter))]
    [property: TypeConverter(typeof(YesNoBooleanConverter))]
    bool? ConfDeniedExpired,

    [param: Name(CoverPage.Fields.DateDeniedExpired)]
    [property: Name(CoverPage.Fields.DateDeniedExpired)]
    DateOnly? DateDeniedExpired,

    [param: Name(CoverPage.Fields.DateReported)]
    [property: Name(CoverPage.Fields.DateReported)]
    DateOnly? DateReported,

    [param: Name(CoverPage.Fields.ReasonForNonconfidentiality)]
    [property: Name(CoverPage.Fields.ReasonForNonconfidentiality)]
    string? ReasonForNonconfidentiality,

    [param: Name(CoverPage.Fields.FilingManagerName)]
    [property: Name(CoverPage.Fields.FilingManagerName)]
    string FilingManagerName,

    [param: Name(CoverPage.Fields.FilingManagerStreet1)]
    [property: Name(CoverPage.Fields.FilingManagerStreet1)]
    string FilingManagerStreet1,

    [param: Name(CoverPage.Fields.FilingManagerStreet2)]
    [property: Name(CoverPage.Fields.FilingManagerStreet2)]
    string FilingManagerStreet2,

    [param: Name(CoverPage.Fields.FilingManagerCity)]
    [property: Name(CoverPage.Fields.FilingManagerCity)]
    string FilingManagerCity,

    [param: Name(CoverPage.Fields.FilingManagerStateOrCountry)]
    [property: Name(CoverPage.Fields.FilingManagerStateOrCountry)]
    string FilingManagerStateOrCountry,

    [param: Name(CoverPage.Fields.FilingManagerZipCode)]
    [property: Name(CoverPage.Fields.FilingManagerZipCode)]
    string FilingManagerZipCode,

    [param: Name(CoverPage.Fields.ReportType)]
    [property: Name(CoverPage.Fields.ReportType)]
    [param: TypeConverter(typeof(EnumMemberConverter<ReportType>))]
    [property: TypeConverter(typeof(EnumMemberConverter<ReportType>))]
    ReportType ReportType,

    [param: Name(CoverPage.Fields.Form13FFileNumber)]
    [property: Name(CoverPage.Fields.Form13FFileNumber)]
    string? Form13FFileNumber,

    [param: Name(CoverPage.Fields.CrdNumber)]
    [property: Name(CoverPage.Fields.CrdNumber)]
    string? CrdNumber,

    [param: Name(CoverPage.Fields.SecFileNumber)]
    [property: Name(CoverPage.Fields.SecFileNumber)]
    string? SecFileNumber,

    [param: Name(CoverPage.Fields.ProvideInfoForInstruction5)]
    [property: Name(CoverPage.Fields.ProvideInfoForInstruction5)]
    [param: TypeConverter(typeof(YesNoBooleanConverter))]
    [property: TypeConverter(typeof(YesNoBooleanConverter))]
    bool ProvideInfoForInstruction5,

    [param: Name(CoverPage.Fields.AdditionalInformation)]
    [property: Name(CoverPage.Fields.AdditionalInformation)]
    string? AdditionalInformation)
{
    /// <summary>
    /// Gets the file name
    /// </summary>
    public const string FileName = "COVERPAGE.TSV";

    /// <summary>
    /// Names of the fields
    /// </summary>
    public static class Fields
    {
        public const string AccessionNumber = "ACCESSION_NUMBER";
        public const string ReportCalendarOrQuarter = "REPORTCALENDARORQUARTER";
        public const string IsAmendment = "ISAMENDMENT";
        public const string AmendmentNumber = "AMENDMENTNO";
        public const string AmendmentType = "AMENDMENTTYPE";
        public const string ConfDeniedExpired = "CONFDENIEDEXPIRED";
        public const string DateDeniedExpired = "DATEDENIEDEXPIRED";
        public const string DateReported = "DATEREPORTED";
        public const string ReasonForNonconfidentiality = "REASONFORNONCONFIDENTIALITY";
        public const string FilingManagerName = "FILINGMANAGER_NAME";
        public const string FilingManagerStreet1 = "FILINGMANAGER_STREET1";
        public const string FilingManagerStreet2 = "FILINGMANAGER_STREET2";
        public const string FilingManagerCity = "FILINGMANAGER_CITY";
        public const string FilingManagerStateOrCountry = "FILINGMANAGER_STATEORCOUNTRY";
        public const string FilingManagerZipCode = "FILINGMANAGER_ZIPCODE";
        public const string ReportType = "REPORTTYPE";
        public const string Form13FFileNumber = "FORM13FFILENUMBER";
        public const string CrdNumber = "CRDNUMBER";
        public const string SecFileNumber = "SECFILENUMBER";
        public const string ProvideInfoForInstruction5 = "PROVIDEINFOFORINSTRUCTION5";
        public const string AdditionalInformation = "ADDITIONALINFORMATION";
    }
}