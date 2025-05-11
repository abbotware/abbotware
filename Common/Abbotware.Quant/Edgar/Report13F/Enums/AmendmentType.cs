// -----------------------------------------------------------------------
// <copyright file="AmendmentType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Edgar.Report13F;

using System.Runtime.Serialization;

/// <summary>
/// The 13F Amendment type
/// </summary>
public enum AmendmentType
{
    /// <summary>
    /// Amendment type is a restatement
    /// </summary>
    [EnumMember(Value = "RESTATEMENT")]
    Restatement,

    /// <summary>
    /// Amendment type is for new holdings
    /// </summary>
    [EnumMember(Value = "NEW HOLDINGS")]
    NewHoldings,
}
