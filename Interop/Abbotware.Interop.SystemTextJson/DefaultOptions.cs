// -----------------------------------------------------------------------
// <copyright file="DefaultOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.SystemTextJson;

using System.Text.Json;
using System.Text.Json.Serialization;

public static class DefaultOptions
{
    public static readonly JsonSerializerOptions EnforceStructure = new JsonSerializerOptions()
    {
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
        PropertyNameCaseInsensitive = false,
    };
}
