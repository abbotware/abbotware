// -----------------------------------------------------------------------
// <copyright file="IFieldBasedParser{TRecord}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints.Text
{
    using System.Collections.Generic;

    /// <summary>
    /// interface for a field based parser
    /// </summary>
    /// <typeparam name="TRecord">type of data</typeparam>
    public interface IFieldBasedParser<TRecord> : IParser<TRecord>
    {
        /// <summary>
        /// Gets the column headers
        /// </summary>
        /// <returns>list of column headers</returns>
        IEnumerable<string> FieldHeaders();
    }
}