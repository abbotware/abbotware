// -----------------------------------------------------------------------
// <copyright file="IPrimaryKey.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Entity
{
    /// <summary>
    /// Interface that provides a primary key
    /// </summary>
    public interface IPrimaryKey
    {
        /// <summary>
        /// Gets the key values used in 'Find' operations on DbSets
        /// </summary>
        /// <returns>object array containg key values</returns>
        object[] ToEntityFindKeyValues();
    }
}
