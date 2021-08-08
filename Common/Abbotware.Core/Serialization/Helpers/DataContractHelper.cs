// -----------------------------------------------------------------------
// <copyright file="DataContractHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Serialization.Helpers
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;

    /// <summary>
    /// DataContractSerializer Helper methods
    /// </summary>
    public static class DataContractHelper
    {
        /// <summary>
        ///     serializes a TObject to byte[] using the DataContractSerializer
        /// </summary>
        /// <typeparam name="TObject">Object Type</typeparam>
        /// <param name="extended">object being extended</param>
        /// <returns>byte[] contain a serialized TObject</returns>
        public static byte[] ToXmlByteArrayViaDataContract<TObject>(this TObject extended)
        {
            Arguments.NotNull(extended, nameof(extended));

            using var memStream = new MemoryStream();

            var serializer = new DataContractSerializer(extended!.GetType());
            serializer.WriteObject(memStream, extended);

            var retVal = memStream.ToArray();

            return retVal;
        }

        /// <summary>
        ///     deserializes a byte[] using DataContractSerializer
        /// </summary>
        /// <typeparam name="TObject">Object Type</typeparam>
        /// <param name="extended">object being extended</param>
        /// <returns>Instance of deserialized TObject</returns>
        public static TObject? DeserializeViaDataContract<TObject>(this byte[] extended)
        {
            Arguments.NotNull(extended, nameof(extended));

            return (TObject?)extended.DeserializeViaDataContract(typeof(TObject), Array.Empty<Type>());
        }

        /// <summary>
        ///     deserializes a byte[] using DataContractSerializer
        /// </summary>
        /// <param name="extended">object being extended</param>
        /// <param name="expectedType">type information for deserialization</param>
        /// <param name="additionalTypeInfo">additional types for deserialization</param>
        /// <returns>Instance of deserialized TObject</returns>
        public static object? DeserializeViaDataContract(this byte[] extended, Type expectedType, params Type[] additionalTypeInfo)
        {
            Arguments.NotNull(extended, nameof(extended));

            using var memstream = new MemoryStream(extended);
            var serializer = new DataContractSerializer(expectedType, additionalTypeInfo);

            var obj = serializer.ReadObject(memstream);

            return obj;
        }
    }
}
