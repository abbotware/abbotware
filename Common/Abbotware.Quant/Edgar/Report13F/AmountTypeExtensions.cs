// -----------------------------------------------------------------------
// <copyright file="AmountTypeExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Edgar.Report13F;

/// <summary>
/// Edgar Amount Type Extension methods
/// </summary>
public static class AmountTypeExtensions
{
    /// <summary>
    /// Convert AmountType back to char
    /// </summary>
    /// <param name="extended">enum</param>
    /// <returns>char</returns>
    public static char? ToChar(this AmountType extended)
        => ((AmountType?)extended).ToChar();

    /// <summary>
    /// Convert AmountType back to char
    /// </summary>
    /// <param name="extended">enum</param>
    /// <returns>char</returns>
    public static char? ToChar(this AmountType? extended)
        => extended switch
        {
            AmountType.Shares => 'S',
            AmountType.Principal => 'P',
            _ => null,
        };

    /// <summary>
    /// Convert PutCallType back to char
    /// </summary>
    /// <param name="extended">enum</param>
    /// <returns>char</returns>
    public static char? ToChar(this PutCallType? extended)
        => extended switch
        {
            PutCallType.Put => 'P',
            PutCallType.Call=> 'C',
            PutCallType.Indexed => 'I',
            _ => null,
        };

    /// <summary>
    /// Convert AmendmentType back to char
    /// </summary>
    /// <param name="extended">enum</param>
    /// <returns>char</returns>
    public static char? ToChar(this AmendmentType extended)
        => ((AmendmentType?)extended).ToChar();

    /// <summary>
    /// Convert AmendmentType back to char
    /// </summary>
    /// <param name="extended">enum</param>
    /// <returns>char</returns>
    public static char? ToChar(this AmendmentType? extended)
        => extended switch
        {
            AmendmentType.NewHoldings => 'N',
            AmendmentType.Restatement => 'R',
            _ => null,
        };

    /// <summary>
    /// Convert ReportType back to char
    /// </summary>
    /// <param name="extended">enum</param>
    /// <returns>char</returns>
    public static char? ToChar(this ReportType extended)
        => ((ReportType?)extended).ToChar();

    /// <summary>
    /// Convert ReportType back to char
    /// </summary>
    /// <param name="extended">enum</param>
    /// <returns>char</returns>
    public static char? ToChar(this ReportType? extended)
        => extended switch
        {
            ReportType.Combination => 'C',
            ReportType.Holdings => 'H',
            ReportType.Notice => 'N',
            _ => null,
        };

    /// <summary>
    /// Convert SubmissionType back to char
    /// </summary>
    /// <param name="extended">enum</param>
    /// <returns>char</returns>
    public static char? ToChar(this SubmissionType extended)
    => ((SubmissionType?)extended).ToChar();

    /// <summary>
    /// Convert SubmissionType back to char
    /// </summary>
    /// <param name="extended">enum</param>
    /// <returns>char</returns>
    public static char? ToChar(this SubmissionType? extended)
        => extended switch
        {
            SubmissionType.Holdings => 'H',
            SubmissionType.HoldingsAmendment => 'H',
            SubmissionType.Notice => 'N',
            SubmissionType.NoticeAmendment => 'N',
            _ => null,
        };
}