// -----------------------------------------------------------------------
// <copyright file="Common.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Security
{
    using System;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Common hashing code
    /// </summary>
    internal static class Common
    {
        /// <summary>
        /// Hash data
        /// </summary>
        /// <param name="data">data to hash</param>
        /// <param name="algorithm">hash algorthim</param>
        /// <param name="format">output format</param>
        /// <returns>hash of data</returns>
        internal static string Hash(string data, HashAlgorithm algorithm, HashStringFormat format)
        {
            // convert the input text to array of bytes
            var hashData = algorithm.ComputeHash(Encoding.Default.GetBytes(data));

            // create new instance of StringBuilder to save hashed data
            var returnValue = new StringBuilder();

            var stringFormat = string.Empty;

            switch (format)
            {
                case HashStringFormat.Hex:
                    stringFormat = "x2";
                    break;
                default:
                    throw new InvalidOperationException($"unexpected hash format:{format}");
            }

            // loop for each byte and add it to StringBuilder
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString(stringFormat, CultureInfo.InvariantCulture));
            }

            // return hashed string
            return returnValue.ToString().ToUpperInvariant();
        }
    }
}