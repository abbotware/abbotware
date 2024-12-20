// -----------------------------------------------------------------------
// <copyright file="ErrorMessage.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.SecApi.Model;

/// <summary>
/// Error Message
/// </summary>
/// <param name="Status">status code</param>
/// <param name="Error">error text</param>
public record ErrorMessage(
    long Status,
    string Error);
