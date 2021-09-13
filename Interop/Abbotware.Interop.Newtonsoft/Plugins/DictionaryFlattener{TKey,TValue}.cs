// -----------------------------------------------------------------------
// <copyright file="DictionaryFlattener{TKey,TValue}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Newtonsoft.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abbotware.Core;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Flats a Dictionary / Array into a list
    /// </summary>
    /// <typeparam name="TKey">key type</typeparam>
    /// <typeparam name="TValue">value type</typeparam>
    public class DictionaryFlattener<TKey, TValue> : JsonConverter
    {
        private readonly KeyValueConverter<TKey, TValue>? converter;

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryFlattener{TKey, TValue}"/> class.
        /// </summary>
        public DictionaryFlattener()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryFlattener{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="converterType">type of the key value converter</param>
        public DictionaryFlattener(Type? converterType)
        {
            if (converterType != null)
            {
                this.converter = (KeyValueConverter<TKey, TValue>)Activator.CreateInstance(converterType);
            }
        }

        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Dictionary<TKey, TValue>);
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            serializer = Arguments.EnsureNotNull(serializer, nameof(serializer));

            var val = serializer!.Deserialize<Dictionary<TKey, TValue>>(reader);

            if (this.converter != null)
            {
                return val.Select(x => this.converter.Convert(x)).ToList();
            }

            return val!.Values.ToList();
        }
    }
}