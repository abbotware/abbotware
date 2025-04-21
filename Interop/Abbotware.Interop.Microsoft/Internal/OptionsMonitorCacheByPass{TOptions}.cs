// -----------------------------------------------------------------------
// <copyright file="OptionsMonitorCacheByPass{TOptions}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Microsoft.Internal;

using System;
using global::Microsoft.Extensions.Options;

/// <summary>
/// specialized class to bypass caching
/// </summary>
/// <typeparam name="TOptions">options type</typeparam>
public class OptionsMonitorCacheByPass<TOptions>
  : IOptionsMonitorCache<TOptions>
  where TOptions : class
{
    /// <inheritdoc/>
    public void Clear()
    {
    }

    /// <inheritdoc/>
    public TOptions GetOrAdd(string? name, Func<TOptions> createOptions)
        => createOptions();

    /// <inheritdoc/>
    public bool TryAdd(string? name, TOptions options)
        => true;

    /// <inheritdoc/>
    public bool TryRemove(string? name)
        => true;
}
