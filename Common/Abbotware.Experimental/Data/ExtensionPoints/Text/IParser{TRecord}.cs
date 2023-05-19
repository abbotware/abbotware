// -----------------------------------------------------------------------
// <copyright file="IParser{TRecord}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints.Text
{
    using System.Collections.Generic;

    /// <summary>
    /// Parses the data source
    /// </summary>
    /// <typeparam name="TRecord">type of data</typeparam>
    public interface IParser<TRecord>
    {
        /// <summary>
        /// Parses the data source
        /// </summary>
        /// <returns>parsed records</returns>
        IEnumerable<TRecord> Parse();
    }
}