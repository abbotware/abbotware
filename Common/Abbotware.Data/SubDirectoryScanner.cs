// -----------------------------------------------------------------------
// <copyright file="SubDirectoryScanner.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Data;

using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Abbotware.Core.Chrono;
using Abbotware.Core.Extensions;
using Abbotware.Core.Objects;
using Microsoft.Extensions.Logging;

/// <summary>
/// Sub Directory File Scanner
/// </summary>
/// <param name="root">root directory</param>
/// <param name="logger">injected loggers</param>
public class SubDirectoryScanner(DirectoryInfo root, ILogger<SubDirectoryScanner> logger)
    : BaseComponent(logger)
{
    private readonly Dictionary<IReadOnlySet<string>, Func<FileInfo[], CancellationToken, ValueTask>> callbacks = [];

    private readonly HashSet<string> ignoredFolders = new(StringComparer.InvariantCultureIgnoreCase);

    private readonly HashSet<string> registeredFiles = new(StringComparer.InvariantCultureIgnoreCase);

    /// <summary>
    /// Top Level Folder to ignore
    /// </summary>
    /// <param name="name">folder name</param>
    /// <returns>this for fluent builder chaining</returns>
    public SubDirectoryScanner IgnoreFolder(string name)
    {
        _ = this.ignoredFolders.Add(name);

        return this;
    }

    /// <summary>
    /// Register a callback function for the with the specified name
    /// </summary>
    /// <param name="function">callback function</param>
    /// <param name="name">file name</param>
    /// <returns>this for fluent builder chaining</returns>
    /// <exception cref="ArgumentException">if file already registered</exception>
    public SubDirectoryScanner ForFileNamed(Func<FileInfo, CancellationToken, ValueTask> function, string name)
    {
        var hs = new string[] { name }.ToFrozenSet(StringComparer.InvariantCultureIgnoreCase);

        this.ThrowIfAlreadyRegistered(hs);

        this.callbacks.Add(hs, (hs, ct) => function(hs.Single(), ct));

        return this;
    }

    /// <summary>
    /// Register a callback function for the with the specified name
    /// </summary>
    /// <param name="function">callback function</param>
    /// <param name="names">file names</param>
    /// <returns>this for fluent builder chaining</returns>
    /// <exception cref="ArgumentException">if file already registered</exception>
    public SubDirectoryScanner ForFilesNamed(Func<FileInfo[], CancellationToken, ValueTask> function, params string[] names)
    {
        var hs = names.ToFrozenSet(StringComparer.InvariantCultureIgnoreCase);

        this.ThrowIfAlreadyRegistered(hs);

        this.callbacks.Add(hs, function);

        return this;
    }

    /// <summary>
    /// Process the subdirectories and files
    /// </summary>
    /// <param name="ct">cancellation token</param>
    /// <returns>async handle</returns>
    public ValueTask Process(CancellationToken ct)
        => this.Process(Environment.ProcessorCount, ct);

    /// <summary>
    /// Process the subdirectories and files
    /// </summary>
    /// <param name="maxParallelism">max parallelism</param>
    /// <param name="ct">cancellation token</param>
    /// <returns>async handle</returns>
    public async ValueTask Process(int maxParallelism, CancellationToken ct)
    {
        var po = new ParallelOptions
        {
            MaxDegreeOfParallelism = maxParallelism,
            CancellationToken = ct,
        };

        var dirs = root.GetDirectories();

        this.Logger.Info($"Found {dirs.Length} directories");

        var ts = Stopwatch.StartNew();

        await Parallel.ForEachAsync(dirs, po, async (d, ct) =>
        {
            if (this.ignoredFolders.Contains(d.Name))
            {
                this.Logger.Info($"skipped:{d.FullName}");
                return;
            }

            var files = d.GetFiles()
                .ToDictionary(x => x.Name, x => x, StringComparer.InvariantCultureIgnoreCase);

            foreach (var sets in this.callbacks)
            {
                var batch = new List<FileInfo>(sets.Key.Count);

                foreach (var f in sets.Key)
                {
                    var fi = files.RemoveOrThrow(f);
                    batch.Add(fi);
                }

                var callback = sets.Value;

                await callback([.. batch], ct)
                    .ConfigureAwait(false);
            }
        }).ConfigureAwait(false);

        this.Logger.Info($"Done:{ts.Elapsed}");
    }

    private void ThrowIfAlreadyRegistered(IReadOnlySet<string> hs)
    {
        foreach (var f in hs)
        {
            if (this.registeredFiles.Contains(f))
            {
                throw new ArgumentException($"{f} already registered ");
            }
        }
    }
}
