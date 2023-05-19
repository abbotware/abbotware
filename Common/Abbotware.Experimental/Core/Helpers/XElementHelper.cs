// -----------------------------------------------------------------------
// <copyright file="XElementHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Helpers
{
    using System;
    using System.IO;
    using System.Xml.Linq;

    /// <summary>
    ///     Helper methods for XElement
    /// </summary>
    public static class XElementHelper
    {
        /// <summary>
        ///     Loads an XElement from a file
        /// </summary>
        /// <param name="filePath">path to file</param>
        /// <returns>XElement loaded with data from the file</returns>
        public static XElement LoadFromFile(string filePath)
        {
            Arguments.NotNullOrWhitespace(filePath, nameof(filePath));

            var actualPath = Path.Combine(Environment.CurrentDirectory, filePath);

            var uri = new Uri(actualPath);

            return XElementHelper.LoadFromFile(uri);
        }

        /// <summary>
        ///     Loads an XElement from a file
        /// </summary>
        /// <param name="fileUri">Uri to file</param>
        /// <returns>XElement loaded with data from the file</returns>
        public static XElement LoadFromFile(Uri fileUri)
        {
            Arguments.NotNull(fileUri, nameof(fileUri));

            return XElement.Load(fileUri.AbsoluteUri);
        }

        /// <summary>
        ///     Loads an XElement from a String
        /// </summary>
        /// <param name="xmlData">xml data in string</param>
        /// <returns>XElement loaded with data from the string</returns>
        public static XElement LoadFromString(string xmlData)
        {
            Arguments.NotNullOrWhitespace(xmlData, nameof(xmlData));

            using var textreader = new StringReader(xmlData);

            return XElement.Load(textreader);
        }
    }
}