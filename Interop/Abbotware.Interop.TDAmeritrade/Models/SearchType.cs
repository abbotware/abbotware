// -----------------------------------------------------------------------
// <copyright file="SearchType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    ///  Type of Search request
    /// </summary>
    public enum SearchType
    {
        /// <summary>
        /// Retrieve instrument data of a specific symbol or cusip
        /// </summary>
        [EnumMember(Value = "symbol-search")]
        SymbolSearch,

        /// <summary>
        /// Retrieve instrument data for all symbols matching regex.
        /// </summary>
        /// <example>
        /// Example: symbol=XYZ.* will return all symbols beginning with XYZ
        /// </example>
        [EnumMember(Value = "symbol-regex")]
        SymbolRegex,

        /// <summary>
        ///  Retrieve instrument data for instruments whose description contains the word supplied
        /// </summary>
        /// <example>Example: symbol=FakeCompany will return all instruments with FakeCompany in the description.</example>
        [EnumMember(Value = "desc-search")]
        DescSearch,

        /// <summary>
        ///  Search description with full regex support
        /// </summary>
        /// <example>
        /// Example: symbol=XYZ.[A-C] returns all instruments whose descriptions contain a word beginning with XYZ followed by a character A through C.
        /// </example>
        [EnumMember(Value = "desc-regex")]
        DescRegex,

        /// <summary>
        /// Returns fundamental data for a single instrument specified by exact symbol.'
        /// </summary>
        Fundamental,
    }
}
