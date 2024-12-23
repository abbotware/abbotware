namespace Abbotware.Quant.Edgar.Report13F;

using System.Runtime.Serialization;

/// <summary>
/// EDGAR Submission Type
/// </summary>
/// <remarks>see ReportType</remarks>
public enum SubmissionType
{
    /// <summary>
    /// 13F Holdings Report OR 13F Combination Report
    /// </summary>
    [EnumMember(Value = "13F-HR")]
    Holdings,

    /// <summary>
    /// 13F Notice
    /// </summary>
    [EnumMember(Value = "13F-NT")]
    Notice,

    /// <summary>
    /// Amendment for a 13F Holdings Report OR 13F Combination Report
    /// </summary>
    [EnumMember(Value = "13F-HR/A")]
    HoldingsAmendment,

    /// <summary>
    /// Amendment for a 13F Notice
    /// </summary>
    [EnumMember(Value = "13F-NT/A")]
    NoticeAmendment,
}
