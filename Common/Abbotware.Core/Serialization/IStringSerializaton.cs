// -----------------------------------------------------------------------
// <copyright file="IStringSerializaton.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Serialization
{
    using System.Text;

    /// <summary>
    /// protocol type for serialization of objects into strings
    /// </summary>
    public interface IStringSerializaton : ISerialization<string>
    {
        /// <summary>
        /// Gets the string encoding type
        /// </summary>
        Encoding Encoding { get; }

        /// <summary>
        /// Gets the mime type for this serializer
        /// </summary>
        string MimeType { get; }
    }
}