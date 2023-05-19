// -----------------------------------------------------------------------
// <copyright file="DictionaryExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    /// <summary>
    ///     IDictionary Extensions methods
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        ///     formats an IDictionary to a string
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="extended">dictionary to convert to a string</param>
        /// <returns>formatted string</returns>
        public static string StringFormat<TKey, TValue>(this IDictionary<TKey, TValue> extended)
        {
            Arguments.NotNull(extended, nameof(extended));

            return DictionaryExtensions.StringFormat(extended, "{0}='{1}'");
        }

        /// <summary>
        ///     formats an IDictionary to a string
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="extended">dictionary to convert to a string</param>
        /// <param name="format">format string</param>
        /// <returns>formatted string</returns>
        public static string StringFormat<TKey, TValue>(this IDictionary<TKey, TValue> extended, string format)
        {
            Arguments.NotNull(extended, nameof(extended));
            Arguments.NotNullOrWhitespace(format, nameof(format));

            if (!extended.Any())
            {
                return "Dictionary Is Empty";
            }

            var aggregate = extended
                .OfType<DictionaryEntry>()
                .OrderBy(s => s.Key)
                .Aggregate(
                    new StringBuilder(),
                    (sb, kvp) =>
                    {
                        if (kvp.Value is byte[])
                        {
                            sb.AppendFormat(CultureInfo.InvariantCulture, format, kvp.Key, Encoding.ASCII.GetString(kvp.Value as byte[] ?? Array.Empty<byte>()));
                        }
                        else if (kvp.Value is IDictionary)
                        {
                            sb.AppendFormat(CultureInfo.InvariantCulture, format, kvp.Key, (kvp.Value as IDictionary)?.StringFormat());
                        }
                        else
                        {
                            sb.AppendFormat(CultureInfo.InvariantCulture, format, kvp.Key, kvp.Value);
                        }

                        return sb;
                    });

            var retVal = aggregate.ToString();

            if (retVal.IsBlank())
            {
                return "Dictionary Is Empty";
            }

            return retVal;
        }

        /// <summary>
        ///     formats an IDictionary to a string
        /// </summary>
        /// <param name="dictionary">dictionary to convert to a string</param>
        /// <returns>formatted string</returns>
        public static string StringFormat(this IDictionary dictionary)
        {
            Arguments.NotNull(dictionary, nameof(dictionary));

            return DictionaryExtensions.StringFormat(dictionary, "{0}='{1}'");
        }

        /// <summary>
        ///     formats an IDictionary to a string
        /// </summary>
        /// <param name="dictionary">dictionary to convert to a string</param>
        /// <param name="format">format string</param>
        /// <returns>formatted string</returns>
        public static string StringFormat(this IDictionary dictionary, string format)
        {
            Arguments.NotNull(dictionary, nameof(dictionary));
            Arguments.NotNullOrWhitespace(format, nameof(format));

            var aggregate = dictionary
                .OfType<DictionaryEntry>()
                .OrderBy(s => s.Key)
                .Aggregate(
                    new StringBuilder(),
                    (sb, kvp) =>
                    {
                        switch (kvp.Value)
                        {
                            case byte[] b:
                                sb.AppendFormat(CultureInfo.InvariantCulture, format, kvp.Key, Encoding.ASCII.GetString(b));
                                break;
                            case IDictionary d:
                                sb.AppendFormat(CultureInfo.InvariantCulture, format, kvp.Key, DictionaryExtensions.StringFormat(d));
                                break;
                            case null:
                            default:
                                sb.AppendFormat(CultureInfo.InvariantCulture, format, kvp.Key, kvp.Value);
                                break;
                        }

                        return sb;
                    });

            // Would never have done this if Code Contracts didnt point out the fact that I had a possible null reference in the above LINQ code!
            // I orignally put the ToString() on the above composed LINQ, but that would have been an error if the dictionary passed in was empty
            if (aggregate == null)
            {
                return "Dictionary Is Empty";
            }

            var retVal = aggregate.ToString();

            if (retVal.IsBlank())
            {
                return "Dictionary Is Empty";
            }

            return retVal;
        }
    }
}