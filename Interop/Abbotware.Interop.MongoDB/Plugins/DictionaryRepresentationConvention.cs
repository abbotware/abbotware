// -----------------------------------------------------------------------
// <copyright file="DictionaryRepresentationConvention.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.MongoDB
{
    using Abbotware.Core;
    using global::MongoDB.Bson.Serialization;
    using global::MongoDB.Bson.Serialization.Conventions;
    using global::MongoDB.Bson.Serialization.Options;

    /// <summary>
    ///     convention for dictionary serialization
    /// </summary>
    public class DictionaryRepresentationConvention : ConventionBase, IMemberMapConvention
    {
        /// <summary>
        ///     dictionary representation
        /// </summary>
        private readonly DictionaryRepresentation dictionaryRepresentation;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DictionaryRepresentationConvention" /> class.
        /// </summary>
        /// <param name="dictionaryRepresentation">dictionary representation</param>
        public DictionaryRepresentationConvention(DictionaryRepresentation dictionaryRepresentation)
        {
            this.dictionaryRepresentation = dictionaryRepresentation;
        }

        /// <inheritdoc />
        public void Apply(BsonMemberMap memberMap)
        {
            memberMap = Arguments.EnsureNotNull(memberMap, nameof(memberMap));

            memberMap.SetSerializer(this.ConfigureSerializer(memberMap.GetSerializer()));
        }

        /// <summary>
        ///     configure serializer
        /// </summary>
        /// <param name="serializer">input</param>
        /// <returns>output</returns>
        private IBsonSerializer ConfigureSerializer(IBsonSerializer serializer)
        {
            if (serializer is IDictionaryRepresentationConfigurable dictionaryRepresentationConfigurable)
            {
                serializer = dictionaryRepresentationConfigurable.WithDictionaryRepresentation(this.dictionaryRepresentation);
            }

            if (serializer is IChildSerializerConfigurable childSerializerConfigurable)
            {
                serializer = childSerializerConfigurable.WithChildSerializer(this.ConfigureSerializer(childSerializerConfigurable.ChildSerializer));
            }

            return serializer;
        }
    }
}