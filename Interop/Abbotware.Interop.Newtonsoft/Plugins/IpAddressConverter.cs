﻿// -----------------------------------------------------------------------
// <copyright file="IpAddressConverter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Newtonsoft.Plugins
{
    using System;
    using System.Net;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using global::Newtonsoft.Json;
    using global::Newtonsoft.Json.Linq;

    /// <inheritdoc />
    /// <summary>
    ///     Json converter for ip address
    /// </summary>
    public class IpAddressConverter : JsonConverter<IPAddress>
    {
        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, IPAddress? value, JsonSerializer serializer)
        {
            writer = Arguments.EnsureNotNull(writer, nameof(writer));

            writer.WriteValue(value?.ToString());
        }

        /// <inheritdoc />
        public override IPAddress? ReadJson(JsonReader reader, Type objectType, IPAddress? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            Arguments.NotNull(reader, nameof(reader));

            var token = JToken.Load(reader);

            var s = token.Value<string>();

            if (s.IsBlank())
            {
                return null;
            }

            return IPAddress.Parse(s);
        }
    }
}