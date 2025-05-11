namespace Abbotware.Interop.SystemTextJson;

using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Json Helper methods
/// </summary>
public static class Json
{
    /// <summary>
    /// Json File to Class
    /// </summary>
    /// <typeparam name="T">POCO type</typeparam>
    /// <param name="file">file path</param>
    /// <param name="options">options</param>
    /// <param name="ct">cancellation token</param>
    /// <returns>deserilized data</returns>
    /// <exception cref="FileNotFoundException">if file not exists</exception>
    public static async ValueTask<T?> FromFileAsync<T>(FileInfo file, JsonSerializerOptions options, CancellationToken ct)
    {
        if (!file.Exists)
        {
            throw new FileNotFoundException($"File not found: {file.FullName}");
        }

        using FileStream fs = File.OpenRead(file.FullName);

        return await JsonSerializer.DeserializeAsync<T>(fs, options, ct)
            .ConfigureAwait(false);
    }
}
