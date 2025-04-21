namespace Abbotware.Quant.Edgar.Report13F;

using System.Runtime.Serialization;

/// <summary>
/// Put / Call type
/// </summary>
public enum PutCallType
{
    /// <summary>
    /// Not specified
    /// </summary>
    NotSpecified = 0,

    /// <summary>
    /// Option is a put
    /// </summary>
    [EnumMember(Value = "PUT")]
    Put,

    /// <summary>
    /// Option is a call
    /// </summary>
    [EnumMember(Value = "CALL")]
    Call,

    /// <summary>
    /// Option is an index
    /// </summary>
    [EnumMember(Value = "INDEXED")]
    Indexed,
}
