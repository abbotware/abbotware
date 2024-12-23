namespace Abbotware.Quant.Edgar.Report13F;

using System;
using System.Globalization;
using System.IO;

/// <summary>
/// Edgar Helper Functions
/// </summary>
public static class EdgarHelper
{
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

        return quarter; ;
    }
}