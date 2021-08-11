// -----------------------------------------------------------------------
// <copyright file="JsonHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Newtonsoft
{
    using Abbotware.Core;
    using Abbotware.Interop.Newtonsoft.Plugins;
    using global::Newtonsoft.Json;
    using global::Newtonsoft.Json.Converters;

    /// <summary>
    /// Newtonsoft Json Helper class
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// common settings
        /// </summary>
        private static readonly JsonSerializerSettings Settings = CreateDefaultSettings();

        /// <summary>
        /// Creates the JsonSerializer settings
        /// </summary>
        /// <returns>default settings</returns>
        public static JsonSerializerSettings CreateDefaultSettings()
        {
            var settings = new JsonSerializerSettings();

            AddCommonConverters(settings);
            settings.Formatting = Formatting.Indented;
            settings.MissingMemberHandling = MissingMemberHandling.Error;
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.DateParseHandling = DateParseHandling.DateTime;

            return settings;
        }

        /// <summary>
        /// converts object to JSON string
        /// </summary>
        /// <typeparam name="TObject">object type</typeparam>
        /// <param name="object">object</param>
        /// <returns>JSON text</returns>
        public static string ToString<TObject>(TObject @object)
        {
            return ToString(@object, Settings);
        }

        /// <summary>
        /// converts JSON string to object
        /// </summary>
        /// <typeparam name="TObject">object type</typeparam>
        /// <param name="text">JSON text</param>
        /// <returns>object</returns>
        public static TObject FromString<TObject>(string text)
        {
            return FromString<TObject>(text, Settings);
        }

        /// <summary>
        /// converts object to JSON string
        /// </summary>
        /// <typeparam name="TObject">object type</typeparam>
        /// <param name="object">object</param>
        /// <param name="settings">serializer settings</param>
        /// <returns>JSON text</returns>
        public static string ToString<TObject>(TObject @object, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(@object, settings);
        }

        /// <summary>
        /// converts JSON string to object
        /// </summary>
        /// <typeparam name="TObject">object type</typeparam>
        /// <param name="text">JSON text</param>
        /// <param name="settings">serializer settings</param>
        /// <returns>object</returns>
        public static TObject FromString<TObject>(string text, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<TObject>(text, settings)!;
        }

        /// <summary>
        /// Add common converters to the settings
        /// </summary>
        /// <param name="settings">settings to update</param>
        public static void AddCommonConverters(JsonSerializerSettings settings)
        {
            settings = Arguments.EnsureNotNull(settings, nameof(settings));

            settings.Converters.Add(new IpAddressConverter());
            settings.Converters.Add(new IPEndPointConverter());
            settings.Converters.Add(new StringEnumConverter());
            settings.Converters.Add(new TimeSpanConverter());
            settings.Converters.Add(new NullableConverter());
        }
    }
}