// -----------------------------------------------------------------------
// <copyright file="Submission.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Edgar.Report13F;

using System;
using System.ComponentModel.DataAnnotations;
using Abbotware.Interop.CsvHelper.Plugins;
using CsvHelper.Configuration.Attributes;

/// <summary>
/// Table / File Definition for SUBMISSION.tsv
///
/// SUBMISSION identifies the XML submissions, filer and report information, with each row having the primary key ACCESSION_NUMBER.
/// </summary>
/// <param name="AccessionNumber">Unique identifier assigned by the SEC to each EDGAR submission.</param>
/// <param name="FilingDate">Filing date with the Commission; sourced from EDGAR in (DD-MON-YYYY) format.</param>
/// <param name="SubmissionType">Submission type is either an initial quarterly Form 13F holdings report, notice report or the amendment.</param>
/// <param name="Cik">Filer CIK (Central index key).</param>
/// <param name="PeriodOfReport">Period of report in (DD-MON-YYYY) format.</param>
public record Submission(
    [property:Key]
    [property:Name(Submission.Fields.AccessionNumber)]
    [param:Name(Submission.Fields.AccessionNumber)]
    string AccessionNumber,
    [property:Name(Submission.Fields.FilingDate)]
    [param:Name(Submission.Fields.FilingDate)]
    DateOnly FilingDate,
    [property:Name(Submission.Fields.SubmissionType)]
    [param:Name(Submission.Fields.SubmissionType)]
    [property:TypeConverter(typeof(EnumMemberConverter<SubmissionType>))]
    [param:TypeConverter(typeof(EnumMemberConverter<SubmissionType>))]
    SubmissionType SubmissionType,
    [property:Name(Submission.Fields.Cik)]
    [param:Name(Submission.Fields.Cik)]
    long Cik,
    [property:Name(Submission.Fields.PeriodOfReport)]
    [param:Name(Submission.Fields.PeriodOfReport)]
    DateOnly PeriodOfReport)
{
    /// <summary>
    /// Gets the file name
    /// </summary>
    public const string FileName = "SUBMISSION.TSV";

    /// <summary>
    /// Names of the fields
    /// </summary>
    public static class Fields
    {
        public const string AccessionNumber = "ACCESSION_NUMBER";
        public const string FilingDate = "FILING_DATE";
        public const string SubmissionType = "SUBMISSIONTYPE";
        public const string Cik = "CIK";
        public const string PeriodOfReport = "PERIODOFREPORT";
    }
}