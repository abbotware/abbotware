// -----------------------------------------------------------------------
// <copyright file="ContractType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Options
{
    /// <summary>
    /// Option Contract Type
    /// </summary>
    public enum ContractType
    {
        /// <summary>
        /// Unknown contract type
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Call Contract
        /// </summary>
        Call = 1,

        /// <summary>
        /// Put Contract
        /// </summary>
        Put = 2,
    }
}
