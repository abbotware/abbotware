// -----------------------------------------------------------------------
// <copyright file="IExporter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.ExtensionPoints
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     interface for exporting data to files
    /// </summary>
    public interface IExporter : IDisposable
    {
        /// <summary>
        ///     exports a single object to a report file
        /// </summary>
        /// <typeparam name="TData">type of data to export</typeparam>
        /// <param name="data">data object to export</param>
        /// <returns>path to generated report file</returns>
        Uri ExportSingle<TData>(TData data);

        /// <summary>
        ///     exports a multiple objects to a report file
        /// </summary>
        /// <typeparam name="TData">type of data to export</typeparam>
        /// <param name="data">list of data to export</param>
        /// <returns>path to generated report file</returns>
        Uri ExportMany<TData>(IEnumerable<TData> data);

        /// <summary>
        ///     exports a single object to a report file
        /// </summary>
        /// <typeparam name="TData">type of data to export</typeparam>
        /// <param name="data">data object to export</param>
        /// <param name="prefix">prefix to append to exported file name</param>
        /// <returns>path to generated report file</returns>
        Uri ExportSingle<TData>(TData data, string prefix);

        /// <summary>
        ///     exports a multiple objects to a report file
        /// </summary>
        /// <typeparam name="TData">type of data to export</typeparam>
        /// <param name="data">list of data to export</param>
        /// <param name="prefix">prefix to append to exported file name</param>
        /// <returns>path to generated report file</returns>
        Uri ExportMany<TData>(IEnumerable<TData> data, string prefix);
    }
}