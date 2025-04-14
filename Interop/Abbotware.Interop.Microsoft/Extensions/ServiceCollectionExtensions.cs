// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Microsoft.Extensions;

using System;
using Abbotware.Interop.Microsoft.Internal;
using global::Microsoft.Extensions.DependencyInjection;
using global::Microsoft.Extensions.Options;

/// <summary>
/// Extension methods for IServiceCollection
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds an immutable option class via a callback factory
    /// </summary>
    /// <typeparam name="TOptions">options type</typeparam>
    /// <param name="extended">target for extension method</param>
    /// <param name="factory">options factory</param>
    /// <returns>service collection</returns>
    public static IServiceCollection AddRecordAsOptions<TOptions>(this IServiceCollection extended, Func<string, TOptions> factory)
      where TOptions : class =>
        extended
            .Configure<TOptions>(a => { })
            .AddSingleton<IOptionsFactory<TOptions>>(new CallbackOptionsFactory<TOptions>(factory));
}
