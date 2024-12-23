namespace Abbotware.Quant.Edgar.Report13F;

using System.Runtime.Serialization;

/// <summary>
/// Shares (SH) or principal amount (PRN) type
/// </summary>
public enum AmountType
{
    /// <summary>
    /// not specified
    /// </summary>
    NotSpecified = 0,

    /// <summary>
    /// Amount is in shares
    /// </summary>
    [EnumMember(Value = "SH")]
    Shares,

    /// <summary>
    /// Amount is in principal
    /// </summary>
    [EnumMember(Value = "PRN")]
    Principal,

    /// <summary>
    /// Amount is in index?
    /// </summary>
    [EnumMember(Value = "INDEXED")]
    Indexed,
}
