// -----------------------------------------------------------------------
// <copyright file="CallbackOptionsFactory{TOptions}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Microsoft.Internal;

using System;
using global::Microsoft.Extensions.Options;

/// <summary>
/// Options Factory that uses a callback function as factory
/// </summary>
/// <typeparam name="TOptions">options type</typeparam>
/// <param name="factory">callback factory</param>
public class CallbackOptionsFactory<TOptions>(Func<string, TOptions> factory)
   : IOptionsFactory<TOptions>
   where TOptions : class
{
    /// <inheritdoc/>
    public TOptions Create(string name)
        => factory(name);
}
