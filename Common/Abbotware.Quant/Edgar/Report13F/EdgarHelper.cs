namespace Abbotware.Quant.Edgar.Report13F;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using Abbotware.Interop.CsvHelper;
using CsvHelper.Configuration;

/// <summary>
/// Edgar Helper Functions
/// </summary>
public static class EdgarHelper
{
    /// <summary>
    /// Gets the Csv Configuration
    /// </summary>
    public static readonly CsvConfiguration CsvHelperConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        Delimiter = "\t",
        HasHeaderRecord = true,
        TrimOptions = TrimOptions.Trim,
    };

    /// <summary>
    /// Converts the directory name into a date
    /// </summary>
    /// <param name="directory">directoy</param>
    /// <returns>the date for the folder</returns>
    /// <exception cref="InvalidOperationException">incompatable name/date</exception>
    public static DateOnly DirectoryToDate(DirectoryInfo directory)
    {
        var year = int.Parse(directory.Name.AsSpan(0, 4), CultureInfo.InvariantCulture);
        var q = directory.Name[5];

        var quarter = q switch
        {
            '1' => new DateOnly(year, 3, 31),
            '2' => new DateOnly(year, 6, 30),
            '3' => new DateOnly(year, 9, 30),
            '4' => new DateOnly(year, 12, 31),
            _ => throw new InvalidOperationException($"{q}"),
        };

        return quarter;
    }

    /// <summary>
    /// Reads the file
    /// </summary>
    /// <typeparam name="TRow">row type</typeparam>
    /// <param name="file">file path</param>
    /// <param name="ct">cancellation token</param>
    /// <returns>async rows</returns>
    public static IAsyncEnumerable<TRow> ReadAsAsync<TRow>(FileInfo file, CancellationToken ct)
        => Csv.ParseAsync<TRow>(file, CsvHelperConfiguration, ct);

    /// <summary>
    /// Reads the file
    /// </summary>
    /// <typeparam name="TRow">row type</typeparam>
    /// <param name="file">file path</param>
    /// <returns>rows</returns>
    public static IEnumerable<TRow> Read<TRow>(FileInfo file)
        => Csv.Parse<TRow>(file, CsvHelperConfiguration);
}