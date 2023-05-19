//-----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.Core.Web.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI;

    /// <summary>
    ///     String extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Parser States
        /// </summary>
        private enum State
        {
            /// <summary>
            ///     State for outside an expression
            /// </summary>
            OutsideExpression,

            /// <summary>
            ///     State on an Open Bracket
            /// </summary>
            OnOpenBracket,

            /// <summary>
            ///     State inside an expression
            /// </summary>
            InsideExpression,

            /// <summary>
            ///     State on a Close Bracket
            /// </summary>
            OnCloseBracket,

            /// <summary>
            ///     End State
            /// </summary>
            End
        }

        /// <summary>
        /// converts html attributes to string
        /// </summary>
        /// <param name="attributes">mapping of attributes </param>
        /// <returns>concatenated string of attributes</returns>
        public static string HtmlAttributesToString(IReadOnlyDictionary<string, object> attributes)
        {
            if (attributes == null)
            {
                return string.Empty;
            }

            StringBuilder build = new StringBuilder(256);

            foreach (var attribute in attributes)
            {
                build.AppendFormat(CultureInfo.InvariantCulture, " {0}='{1}' ", attribute.Key, attribute.Value);
            }

            return build.ToString();
        }

        /// <summary>
        ///     formats a string replacing the {Name} with source.Name's value
        /// </summary>
        /// <see href="http://haacked.com/archive/2009/01/14/named-formats-redux.aspx" />
        /// <param name="format">format string</param>
        /// <param name="source">object source</param>
        /// <returns>string with format replacements</returns>
        public static string FormatWithTokens(this string format, object source)
        {
            Contract.Requires<ArgumentException>(!Abbotware.Core.Extensions.StringExtensions.IsNullOrWhiteSpace(format));
            Contract.Requires<ArgumentNullException>(source != null, "source is null");
            Contract.Ensures(Contract.Result<string>() != null);

            var result = new StringBuilder(format.Length * 2);

            using (var reader = new StringReader(format))
            {
                var expression = new StringBuilder();
                var @char = -1;

                var state = State.OutsideExpression;
                do
                {
                    switch (state)
                    {
                        case State.OutsideExpression:
                        {
                            @char = reader.Read();
                            switch (@char)
                            {
                                case -1:
                                    state = State.End;
                                    break;
                                case '{':
                                    state = State.OnOpenBracket;
                                    break;
                                case '}':
                                    state = State.OnCloseBracket;
                                    break;
                                default:
                                    result.Append((char)@char);
                                    break;
                            }

                            break;
                        }

                        case State.OnOpenBracket:
                        {
                            @char = reader.Read();
                            switch (@char)
                            {
                                case -1:
                                    throw new FormatException();
                                case '{':
                                    result.Append('{');
                                    state = State.OutsideExpression;
                                    break;
                                default:
                                    expression.Append((char)@char);
                                    state = State.InsideExpression;
                                    break;
                            }

                            break;
                        }

                        case State.InsideExpression:
                        {
                            @char = reader.Read();
                            switch (@char)
                            {
                                case -1:
                                    throw new FormatException();
                                case '}':
                                    result.Append(StringExtensions.OutExpression(source, expression.ToString()));
                                    expression.Length = 0;
                                    state = State.OutsideExpression;
                                    break;
                                default:
                                    expression.Append((char)@char);
                                    break;
                            }

                            break;
                        }

                        case State.OnCloseBracket:
                        {
                            @char = reader.Read();
                            switch (@char)
                            {
                                case '}':
                                    result.Append('}');
                                    state = State.OutsideExpression;
                                    break;
                                default:
                                    throw new FormatException();
                            }

                            break;
                        }

                        default:
                        {
                            throw new InvalidOperationException("Invalid state.");
                        }
                    }
                }
                while (state != State.End);
            }

            return result.ToString();
        }

        /// <summary>
        ///     Internal expression data binding
        /// </summary>
        /// <param name="source">source object</param>
        /// <param name="expression">expression / property</param>
        /// <returns>the evaluated property for the expression</returns>
        private static string OutExpression(object source, string expression)
        {
            Contract.Requires(expression != null);
            Contract.Requires(source != null);

            var format = string.Empty;

            var colonIndex = expression.IndexOf(':');

            if (colonIndex > 0)
            {
                format = expression.Substring(colonIndex + 1);
                expression = expression.Substring(0, colonIndex);
            }

            try
            {
                if (string.IsNullOrEmpty(format))
                {
                    return (DataBinder.Eval(source, expression) ?? string.Empty).ToString();
                }

                return DataBinder.Eval(source, expression, "{0:" + format + "}") ?? string.Empty;
            }
            catch (HttpException ex)
            {
                throw new FormatException("problem during data binding", ex);
            }
        }
    }
}