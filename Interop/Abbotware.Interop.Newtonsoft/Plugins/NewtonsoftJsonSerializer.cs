// -----------------------------------------------------------------------
// <copyright file="NewtonsoftJsonSerializer.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Newtonsoft.Plugins
{
    using System;
    using System.IO;
    using System.Text;
    using Abbotware.Core.Serialization;
    using global::Newtonsoft.Json;
    using global::Newtonsoft.Json.Serialization;

    /// <summary>
    /// Json Serializer using Newtonsoft / Json.net
    /// </summary>
    public sealed class NewtonsoftJsonSerializer : IBinarySerializaton, IStringSerializaton
    {
        private readonly JsonSerializer serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewtonsoftJsonSerializer"/> class.
        /// </summary>
        public NewtonsoftJsonSerializer()
            : this(Encoding.UTF8, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewtonsoftJsonSerializer"/> class.
        /// </summary>
        /// <param name="stringEncoder">string encoder</param>
        /// <param name="settings">serializer settings</param>
        public NewtonsoftJsonSerializer(Encoding stringEncoder, JsonSerializerSettings settings)
        {
            this.Encoding = stringEncoder;

            if (settings == null)
            {
                var s = new JsonSerializerSettings();
                JsonHelper.AddCommonConverters(s);

                this.serializer = JsonSerializer.Create(s);
            }
            else
            {
                this.serializer = JsonSerializer.Create(settings);
            }
        }

        /// <summary>
        /// Gets the settings with snake case naming set
        /// </summary>
        public static JsonSerializerSettings SnakeCaseNamingSettings { get; } = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy(),
            },
        };

        /// <inheritdoc/>
        public Encoding Encoding { get; }

        /// <inheritdoc/>
        public string MimeType => "application/json";

          /// <summary>
        /// Gets the IBinarySerializaton interface for this object
        /// </summary>
        private IBinarySerializaton BinaryProtocol => this;

        /// <summary>
        /// Gets the IStringSerializaton interface for this object
        /// </summary>
        private IStringSerializaton StringProtocol => this;

        /// <inheritdoc/>
        T IProtocol<byte[]>.Decode<T>(byte[] storage)
        {
            return (T)this.BinaryProtocol.Decode(storage, typeof(T));
        }

        /// <inheritdoc/>
        object ISerialization<byte[]>.Decode(byte[] storage, Type t)
        {
            var s = this.Encoding.GetString(storage);

            return this.StringProtocol.Decode(s, t);
        }

        /// <inheritdoc/>
        T IProtocol<string>.Decode<T>(string storage)
        {
            return (T)this.StringProtocol.Decode(storage, typeof(T));
        }

        /// <inheritdoc/>
        object ISerialization<string>.Decode(string storage, Type type)
        {
            using var sw = new StringReader(storage);

            return this.serializer.Deserialize(sw, type);
        }

        /// <inheritdoc/>
        byte[] IProtocol<byte[]>.Encode<T>(T @object)
        {
            var s = this.StringProtocol.Encode(@object);

            return this.Encoding.GetBytes(s);
        }

        /// <inheritdoc/>
        string IProtocol<string>.Encode<T>(T @object)
        {
            var sb = new StringBuilder();

            using var sw = new StringWriter(sb);

            this.serializer.Serialize(sw, @object);

            return sb.ToString();
        }
    }
}