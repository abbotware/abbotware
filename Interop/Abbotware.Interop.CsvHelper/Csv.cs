// -----------------------------------------------------------------------
// <copyright file="Csv.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.CsvHelper;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Abbotware.Core;
using Abbotware.Core.Data.Serialization.Options;
using Abbotware.Core.Extensions;
using global::CsvHelper;
using global::CsvHelper.Configuration;
using Microsoft.Extensions.Logging;

/// <summary>
///     Helper class that provides ease of use functions for the parser class
/// </summary>
public static class Csv
{
    /// <summary>
    /// Parse a file into C# Models
    /// </summary>
    /// <typeparam name="TRow">row model type</typeparam>
    /// <param name="directory">directory of file</param>
    /// <param name="file">file name</param>
    /// <param name="configuration">csv parsing configuraiton</param>
    /// <returns>parsed rows</returns>
    public static IEnumerable<TRow> Parse<TRow>(DirectoryInfo directory, string file, IReaderConfiguration configuration)
        => Parse<TRow>(new FileInfo(Path.Combine(directory.FullName, file)), configuration);

    /// <summary>
    /// Parse a file into C# Models
    /// </summary>
    /// <typeparam name="TRow">row model type</typeparam>
    /// <param name="file">file info</param>
    /// <param name="configuration">csv parsing configuraiton</param>
    /// <returns>parsed rows</returns>
    public static IEnumerable<TRow> Parse<TRow>(FileInfo file, IReaderConfiguration configuration)
    {
        using var reader = new StreamReader(file.FullName);
        using var csv = new CsvReader(reader, configuration);
        return csv.GetRecords<TRow>().ToList();
    }

    /// <summary>
    /// Parse a file into C# Models
    /// </summary>
    /// <typeparam name="TRow">row model type</typeparam>
    /// <param name="file">file info</param>
    /// <param name="configuration">csv parsing configuraiton</param>
    /// <param name="ct">cancellation token</param>
    /// <returns>parsed rows</returns>
    public static async IAsyncEnumerable<TRow> ParseAsync<TRow>(FileInfo file, IReaderConfiguration configuration, [EnumeratorCancellation] CancellationToken ct)
    {
        using var reader = new StreamReader(file.FullName);
        using var csv = new CsvReader(reader, configuration);

        var rows = csv.GetRecordsAsync<TRow>(ct)
            .ConfigureAwait(false);

        await foreach (var r in rows)
        {
            yield return r;
        }
    }

    /// <summary>
    /// Asynchronously Parse a file into C# Models
    /// </summary>
    /// <typeparam name="TRow">row model type</typeparam>
    /// <param name="file">file info</param>
    /// <param name="configuration">csv parsing configuraiton</param>
    /// <param name="callback">callback action</param>
    /// <param name="ct">cancellation token</param>
    /// <returns>async handle</returns>
    public static async IAsyncEnumerable<TRow> ParseAsync<TRow>(
       FileInfo file,
       CsvConfiguration configuration,
       Func<CsvReader, CancellationToken, TRow> callback,
       [EnumeratorCancellation] CancellationToken ct)
    {
        using var reader = new StreamReader(file.FullName);
        using var csv = new CsvReader(reader, configuration);

        if (configuration.HasHeaderRecord)
        {
            VerifyAction(await csv.ReadAsync().ConfigureAwait(false));
            VerifyAction(csv.ReadHeader());
        }

        while (await csv.ReadAsync().ConfigureAwait(false))
        {
            if (ct.IsCancellationRequested)
            {
                yield break;
            }

            // Call the lambda function to read fields individually
            yield return callback(csv, ct);
        }

        static void VerifyAction(bool readHeader)
        {
            if (!readHeader)
            {
                throw new InvalidOperationException("failed on header read");
            }
        }
    }

    /// <summary>
    /// Asynchronously Parse a file into C# Models
    /// </summary>
    /// <typeparam name="TRow">row model type</typeparam>
    /// <typeparam name="TState">state type</typeparam>
    /// <param name="file">file info</param>
    /// <param name="configuration">csv parsing configuraiton</param>
    /// <param name="state">state to pass to callback</param>
    /// <param name="callback">callback action</param>
    /// <param name="ct">cancellation token</param>
    /// <returns>async handle</returns>
    public static async ValueTask ParseWithCallbackAsync<TRow, TState>(FileInfo file, IReaderConfiguration configuration, TState state, Action<TState, TRow> callback, CancellationToken ct)
    {
        using var reader = new StreamReader(file.FullName);
        using var csv = new CsvReader(reader, configuration);

        await foreach (var r in csv.GetRecordsAsync<TRow>(ct).ConfigureAwait(false))
        {
            callback(state, r);
        }
    }

    /// <summary>
    /// Asynchronously write rows (C# Models) to a file
    /// </summary>
    /// <typeparam name="TRow">row model type</typeparam>
    /// <param name="directory">directory of file</param>
    /// <param name="file">file name</param>
    /// <param name="rows">models to write</param>
    /// <param name="configuration">csv parsing configuraiton</param>
    /// <param name="ct">cancellation token</param>
    /// <returns>parsed rows</returns>
    public static ValueTask WriteAsync<TRow>(DirectoryInfo directory, string file, IEnumerable<TRow> rows, IWriterConfiguration configuration, CancellationToken ct)
        => WriteAsync(directory.FileInfo(false, file), rows, configuration, ct);

    /// <summary>
    /// Asynchronously write rows (C# Models) to a file
    /// </summary>
    /// <typeparam name="TRow">row model type</typeparam>
    /// <param name="file">file</param>
    /// <param name="rows">models to write</param>
    /// <param name="configuration">csv parsing configuraiton</param>
    /// <param name="ct">cancellation token</param>
    /// <returns>parsed rows</returns>
    public static async ValueTask WriteAsync<TRow>(FileInfo file, IEnumerable<TRow> rows, IWriterConfiguration configuration, CancellationToken ct)
    {
        using var writer = new StreamWriter(file.FullName);
        using var csv = new CsvWriter(writer, configuration);

        await csv.WriteRecordsAsync(rows, ct)
            .ConfigureAwait(false);
    }

    /// <summary>
    ///     Parses a Csv file using the supplied model
    /// </summary>
    /// <typeparam name="TDataRow">model type for csv file row</typeparam>
    /// <param name="filePath">path to file</param>
    /// <param name="logger">injected logger</param>
    /// <returns>data rows</returns>
    public static IEnumerable<TDataRow> Parse<TDataRow>(string filePath, ILogger logger)
        where TDataRow : new()
    {
        var cfg = new ParserConfiguration();

        return Parse<TDataRow>(filePath, cfg, logger);
    }

    /// <summary>
    ///     Parses a Csv file using the supplied model.  File and Class must match exactly
    /// </summary>
    /// <typeparam name="TDataRow">model type for csv file row</typeparam>
    /// <param name="reader">text reader</param>
    /// <param name="logger">injected logger</param>
    /// <returns>data rows</returns>
    public static IEnumerable<TDataRow> Parse<TDataRow>(TextReader reader, ILogger logger)
        where TDataRow : new()
    {
        Arguments.NotNull(reader, nameof(reader));
        Arguments.NotNull(logger, nameof(logger));

        var cfg = new ParserConfiguration();

        return Parse<TDataRow>(reader, cfg, logger);
    }

    /// <summary>
    ///     Parses a Csv file using the supplied model.  File and Class must match exactly
    /// </summary>
    /// <typeparam name="TDataRow">model type for csv file row</typeparam>
    /// <param name="reader">text reader</param>
    /// <param name="logger">injected logger</param>
    /// <returns>data rows</returns>
    public static IEnumerable<TDataRow> ParseExact<TDataRow>(TextReader reader, ILogger logger)
        where TDataRow : new()
    {
        Arguments.NotNull(reader, nameof(reader));
        Arguments.NotNull(logger, nameof(logger));

        var cfg = new ParserConfiguration
        {
            AllowClassToHaveExtraProperties = false,
            AllowFileToHaveExtraProperties = false,
        };

        return Parse<TDataRow>(reader, cfg, logger);
    }

    /// <summary>
    ///     Parses a Csv file using the supplied model.  File and Class must match exactly
    /// </summary>
    /// <typeparam name="TDataRow">model type for csv file row</typeparam>
    /// <param name="filePath">path to file</param>
    /// <param name="logger">injected logger</param>
    /// <returns>data rows</returns>
    public static IEnumerable<TDataRow> ParseExact<TDataRow>(string filePath, ILogger logger)
        where TDataRow : new()
    {
        var cfg = new ParserConfiguration
        {
            AllowClassToHaveExtraProperties = false,
            AllowFileToHaveExtraProperties = false,
        };

        return Parse<TDataRow>(filePath, cfg, logger);
    }

    /// <summary>
    ///     Parses a Csv file using the supplied model.  File and Class must match exactly
    /// </summary>
    /// <typeparam name="TDataRow">model type for csv file row</typeparam>
    /// <param name="filePath">path to file</param>
    /// <param name="cfg">parser configuration</param>
    /// <param name="logger">injected logger</param>
    /// <returns>data rows</returns>
    private static IEnumerable<TDataRow> Parse<TDataRow>(string filePath, ParserConfiguration cfg, ILogger logger)
        where TDataRow : new()
    {
        logger = Arguments.EnsureNotNull(logger, nameof(logger));

        logger.Info($"Csv File:{filePath}");

        using var reader = new StreamReader(filePath);

        return Parse<TDataRow>(reader, cfg, logger);
    }

    /// <summary>
    ///     Parses a Csv file using the supplied model.  File and Class must match exactly
    /// </summary>
    /// <typeparam name="TDataRow">model type for csv file row</typeparam>
    /// <param name="reader">text reader</param>
    /// <param name="cfg">parser configuration</param>
    /// <param name="logger">injected logger</param>
    /// <returns>data rows</returns>
    private static IEnumerable<TDataRow> Parse<TDataRow>(TextReader reader, ParserConfiguration cfg, ILogger logger)
    where TDataRow : new()
    {
        var c = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            BufferSize = cfg.BufferSize,
            HasHeaderRecord = cfg.HasHeaders,
        };

        using var csv = new CsvReader(reader, c);

        ////csv.Read();

        ////csv.ReadHeader();

        ////csv.ValidateHeader<TDataRow>();

        ////csv.Read();

        var records = csv.GetRecords<TDataRow>();

        logger.Info($"rows parsed::{records.Count()}");

        return records;
    }
}