// -----------------------------------------------------------------------
// <copyright file="LogLocationDetail.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Logging;

using System;

/// <summary>
/// Log Location Details
/// </summary>
/// <param name="Line">line number</param>
/// <param name="Member">class member name</param>
/// <param name="File">file path</param>
/// <param name="Message">message function</param>
public record class LogLocationDetail(int? Line, string? Member, string? File, Func<string> Message);
