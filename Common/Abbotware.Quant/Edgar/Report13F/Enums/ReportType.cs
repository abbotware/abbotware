// -----------------------------------------------------------------------
// <copyright file="ReportType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Edgar.Report13F;

using System.Runtime.Serialization;

/// <summary>
/// EDGAR Report Type contained within the submission
/// </summary>
/// <remarks>see ReportType</remarks>
public enum ReportType
{
    /// <summary>
    /// Not specified
    /// </summary>
    NotSpecified = 0,

    /// <summary>
    /// 13F Holdings Report
    /// </summary>
    [EnumMember(Value = "13F HOLDINGS REPORT")]
    Holdings,

    /// <summary>
    /// 13F Notice
    /// </summary>
    [EnumMember(Value = "13F NOTICE")]
    Notice,

    /// <summary>
    /// 13F Combination Report
    /// </summary>
    [EnumMember(Value = "13F COMBINATION REPORT")]
    Combination,
}
