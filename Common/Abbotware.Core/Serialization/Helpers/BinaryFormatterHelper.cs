// -----------------------------------------------------------------------
// <copyright file="BinaryFormatterHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Serialization.Helpers
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// BinaryFormatter Helper methods
    /// </summary>
    public static class BinaryFormatterHelper
    {
        /// <summary>
        ///     serializes a TObject to byte[] using the BinaryFormatter
        /// </summary>
        /// <typeparam name="TObject">Object Type</typeparam>
        /// <param name="extended">object being extended</param>
        /// <returns>byte[] contain a serialized TObject</returns>
        public static byte[] ToXmlByteArrayViaBinaryFormatter<TObject>(this TObject extended)
        {
            Arguments.NotNull(extended, nameof(extended));

            var serializer = new BinaryFormatter();

            using var memStream = new MemoryStream();

            serializer.Serialize(memStream, extended);

            var retVal = memStream.ToArray();

            return retVal;
        }

        /// <summary>
        ///     deserializes a byte[] using BinaryFormatter
        /// </summary>
        /// <typeparam name="TObject">Object Type</typeparam>
        /// <param name="extended">object being extended</param>
        /// <returns>Instance of deserialized TObject</returns>
        public static TObject DeserializeViaBinaryFormatter<TObject>(this byte[] extended)
        {
            Arguments.NotNull(extended, nameof(extended));

            return (TObject)extended.DeserializeViaBinaryFormatter();
        }

        /// <summary>
        ///     deserializes a byte[] using BinaryFormatter
        /// </summary>
        /// <param name="extended">object being extended</param>
        /// <returns>Instance of deserialized TObject</returns>
        public static object DeserializeViaBinaryFormatter(this byte[] extended)
        {
            Arguments.NotNull(extended, nameof(extended));

            using var memstream = new MemoryStream(extended);
            var serializer = new BinaryFormatter();

            var retVal = serializer.Deserialize(memstream);

            return retVal;
        }
    }
}
