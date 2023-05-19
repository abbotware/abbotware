// -----------------------------------------------------------------------
// <copyright file="Helper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.MongoDB
{
    using global::MongoDB.Bson.Serialization.Conventions;
    using global::MongoDB.Bson.Serialization.Options;

    /// <summary>
    /// MongoDB Helper class
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Common Initialize function
        /// </summary>
        public static void Initialize()
        {
            ConventionRegistry.Register("DictionaryRepresentationConvention", new ConventionPack { new DictionaryRepresentationConvention(DictionaryRepresentation.ArrayOfArrays) }, x => true);
        }
    }
}