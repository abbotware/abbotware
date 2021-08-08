// -----------------------------------------------------------------------
// <copyright file="XmlSerializerHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Serialization.Helpers
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using System.Xml.XPath;

    /// <summary>
    /// XmlSerializer Helper methods
    /// </summary>
    public static class XmlSerializerHelper
    {
        /// <summary>
        ///     serializes an object into a string containing xml
        /// </summary>
        /// <param name="extended">object to serialize</param>
        /// <returns>string containing the object data</returns>
        public static string ToStringViaXmlSerializer(this object extended)
        {
            extended = Arguments.EnsureNotNull(extended, nameof(extended));

            var x = XmlSerializerFactory(extended);

            using var writer = new StringWriter(CultureInfo.InvariantCulture);

            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            x.Serialize(writer, extended, ns);

            var retVal = writer.ToString();

            return retVal;
        }

        /// <summary>
        ///     serializes an object into an XDocument
        /// </summary>
        /// <param name="extended">object to serialize</param>
        /// <returns>XDocument containing the object data</returns>
        public static XDocument ToXDocument(this object extended)
        {
            extended = Arguments.EnsureNotNull(extended, nameof(extended));

            var d = new XDocument();

            var x = XmlSerializerFactory(extended);

            using var writer = d.CreateWriter();
            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            x.Serialize(writer, extended, ns);

            return d;
        }

        /// <summary>
        ///     serializes an object into an XElement
        /// </summary>
        /// <param name="extended">object to serialize</param>
        /// <returns>XElement containing the object data</returns>
        public static XElement ToXElement(this object extended)
        {
            extended = Arguments.EnsureNotNull(extended, nameof(extended));

            var retVal = extended.ToXDocument()
                .Root;

            return retVal!;
        }

        /// <summary>
        ///     deserializes a string using XmlSerializer
        /// </summary>
        /// <typeparam name="TObject">Object Type</typeparam>
        /// <param name="extended">string being extended</param>
        /// <returns>Instance of deserialized TObject</returns>
        public static TObject DeserializeViaXmlSerializer<TObject>(this string extended)
        {
            Arguments.NotNull(extended, nameof(extended));

            var bytes = Encoding.Unicode.GetBytes(extended);

            return bytes.DeserializeViaXmlSerializer<TObject>();
        }

        /// <summary>
        ///     deserializes a byte[] using XmlSerializer
        /// </summary>
        /// <typeparam name="TObject">Object Type</typeparam>
        /// <param name="extended">object being extended</param>
        /// <returns>Instance of deserialized TObject</returns>
        public static TObject DeserializeViaXmlSerializer<TObject>(this byte[] extended)
        {
            extended = Arguments.EnsureNotNull(extended, nameof(extended));

            return (TObject)extended.DeserializeViaXmlSerializer(typeof(TObject), Array.Empty<Type>());
        }

        /// <summary>
        ///     deserializes a byte[] using XmlSerializer
        /// </summary>
        /// <param name="extended">object being extended</param>
        /// <param name="expectedType">type information for deserialization</param>
        /// <param name="additionalTypeInfo">additional types for deserialization</param>
        /// <returns>Instance of deserialized TObject</returns>
        public static object DeserializeViaXmlSerializer(this byte[] extended, Type expectedType, params Type[] additionalTypeInfo)
        {
            Arguments.NotNull(extended, nameof(extended));

            using var memstream = new MemoryStream(extended);
            using var reader = XmlReader.Create(memstream);

            // use .Net Serialization
            var serializer = new XmlSerializer(expectedType, additionalTypeInfo);

            var obj = serializer.Deserialize(reader);

            return obj;
        }

        /// <summary>
        ///     serializes a TObject to byte[] using the XmlSerializer
        /// </summary>
        /// <typeparam name="TObject">Object Type</typeparam>
        /// <param name="extended">object being extended</param>
        /// <returns>byte[] contain a serialized TObject</returns>
        public static byte[] ToXmlByteArrayViaXmlSerializer<TObject>(this TObject extended)
        {
            Arguments.NotNull(extended, nameof(extended));

            using var memStream = new MemoryStream();

            var serializer = new XmlSerializer(extended!.GetType());
            serializer.Serialize(memStream, extended);

            var retVal = memStream.ToArray();

            return retVal;
        }

        /// <summary>
        ///     serializes an object into an XmlDocument
        /// </summary>
        /// <param name="extended">object to serialize</param>
        /// <returns>XmlDocument containing the object data</returns>
        public static IXPathNavigable ToXmlDocument(this object extended)
        {
            extended = Arguments.EnsureNotNull(extended, nameof(extended));

            var doc = new XmlDocument();

            var x = XmlSerializerFactory(extended);

            using var writer = new StringWriter(CultureInfo.InvariantCulture);
            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            x.Serialize(writer, extended, ns);

            doc.LoadXml(writer.ToString());

            return doc;
        }

        /// <summary>
        ///     helper method to construct and XmlSerializer (in case we need to change the default serializer in the future)
        /// </summary>
        /// <param name="extended">object to create a serializer for</param>
        /// <returns>xml serializer for the type of extendedObject</returns>
        private static XmlSerializer XmlSerializerFactory(object extended)
        {
            Arguments.NotNull(extended, nameof(extended));

            return new XmlSerializer(extended.GetType());
        }
    }
}
