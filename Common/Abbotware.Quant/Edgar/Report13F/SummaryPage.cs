// -----------------------------------------------------------------------
// <copyright file="SummaryPage.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Edgar.Report13F;

using System;
using System.ComponentModel.DataAnnotations;
using Abbotware.Interop.CsvHelper.Plugins;
using CsvHelper.Configuration.Attributes;

/// <summary>
/// Table / File Definition for SUMMARYPAGE.tsv
///
/// SUBMISSION identifies the XML submissions, filer and report information, with each row having the primary key ACCESSION_NUMBER.
/// </summary>
/// <param name="AccessionNumber">Unique identifier assigned by the SEC to each EDGAR submission.</param>
/// <param name="OtherIncludedManagersCount">Number of other included managers</param>
/// <param name="TableEntryTotal">Table entry total</param>
/// <param name="TableValueTotal">Table value total</param>
/// <param name="IsConfidentialOmitted">Is confidential omitted.</param>
public record SummaryPage(
    [property:Key]
    [property:Name(SummaryPage.Fields.AccessionNumber)]
    [param:Name(SummaryPage.Fields.AccessionNumber)]
    string AccessionNumber,
    [property:Name(SummaryPage.Fields.OtherIncludedManagersCount)]
    [param:Name(SummaryPage.Fields.OtherIncludedManagersCount)]
    int? OtherIncludedManagersCount,
    [property:Name(SummaryPage.Fields.TableEntryTotal)]
    [param:Name(SummaryPage.Fields.TableEntryTotal)]
    int? TableEntryTotal,
    [property:Name(SummaryPage.Fields.TableValueTotal)]
    [param:Name(SummaryPage.Fields.TableValueTotal)]
    long? TableValueTotal,
    [param: Name(SummaryPage.Fields.IsConfidentialOmitted)]
    [property: Name(SummaryPage.Fields.IsConfidentialOmitted)]
    [param: TypeConverter(typeof(YesNoBooleanConverter))]
    [property: TypeConverter(typeof(YesNoBooleanConverter))]
    bool IsConfidentialOmitted)
{
    /// <summary>
    /// Gets the file name
    /// </summary>
    public const string FileName = "SUMMARYPAGE.TSV";

    /// <summary>
    /// Names of the fields
    /// </summary>
    public static class Fields
    {
        public const string AccessionNumber = "ACCESSION_NUMBER";
        public const string OtherIncludedManagersCount = "OTHERINCLUDEDMANAGERSCOUNT";
        public const string TableEntryTotal = "TABLEENTRYTOTAL";
        public const string TableValueTotal = "TABLEVALUETOTAL";
        public const string IsConfidentialOmitted = "ISCONFIDENTIALOMITTED";
    }
}