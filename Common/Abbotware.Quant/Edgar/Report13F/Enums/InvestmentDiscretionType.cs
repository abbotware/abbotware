namespace Abbotware.Quant.Edgar.Report13F;

using System.Runtime.Serialization;

/// <summary>
/// Investment Discretion Type
/// </summary>
public enum InvestmentDiscretionType
{
    /// <summary>
    /// No Discretion
    /// </summary>
    None = 0,

    /// <summary>
    /// Sole Discretion
    /// </summary>
    [EnumMember(Value = "Sole")]
    Sole = 1,

    /// <summary>
    /// Sole discretion
    /// </summary>
    [EnumMember(Value = "????")]
    Shared = 2,

    /// <summary>
    /// Voting authority (for the security)
    /// </summary>
    [EnumMember(Value = "????")]
    VotingAuthority = 3,

    /// <summary>
    /// ????
    /// </summary>
    [EnumMember(Value = "????")]
    DFND,
}
