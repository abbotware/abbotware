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
    [Name("ACCESSION_NUMBER")]
    string AccessionNumber,
    [Name("FILING_DATE")]
    DateOnly FilingDate,
    [Name("SUBMISSIONTYPE"),
    TypeConverter(typeof(EnumMemberConverter<SubmissionType>))]
    SubmissionType SubmissionType,
    [Name("CIK")]
    long Cik,
    [Name("PERIODOFREPORT")]
    DateOnly PeriodOfReport);
