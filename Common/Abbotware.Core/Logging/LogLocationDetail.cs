// -----------------------------------------------------------------------
// <copyright file="LogLocationDetail.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Logging
{
    /// <summary>
    /// Log Location Details
    /// </summary>
    /// <param name="Line">line number</param>
    /// <param name="Member">class member name</param>
    /// <param name="File">file path</param>
    public record class LogLocationDetail(int? Line, string? Member, string? File)
    {
    }
}
