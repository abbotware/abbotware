﻿// -----------------------------------------------------------------------
// <copyright file="BindableConnectionOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Objects.Configuration.Bindable;

using System;
using System.Net;

/// <summary>
/// Connection Options class
/// </summary>
public class BindableConnectionOptions : IConnectionOptions
{
    /// <inheritdoc/>
    public Uri Endpoint { get; set; } = null!;

    /// <inheritdoc/>
    public NetworkCredential? Credential { get; set; }
}
