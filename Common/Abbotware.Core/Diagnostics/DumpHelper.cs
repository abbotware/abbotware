// -----------------------------------------------------------------------
// <copyright file="DumpHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Diagnostics
{
    using System;
    using System.Collections;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Reflection;

    /// <summary>
    ///     Helper class providing helper methods for dumping an object's contents to a string
    /// </summary>
    public static class DumpHelper
    {
        /// <summary>
        ///     writes/walks the element into a context
        /// </summary>
        /// <param name="element">element to write/walk</param>
        /// <param name="context">current dump context</param>
        public static void Write(object? element, DumperContext context)
        {
            context = Arguments.EnsureNotNull(context, nameof(context));

            if (element == null)
            {
                DumpHelper.WriteHeader(context);
                DumpHelper.WriteValue("<NULL>", context);
                return;
            }

            if (context.CurrentDepth == context.MaxDepth)
            {
                DumpHelper.WriteHeader(context);
                DumpHelper.WriteValue(element.ToString()!, context);
                return;
            }

            var elementType = element.GetType();

            if (DumpHelper.IsSimpleType(elementType))
            {
                DumpHelper.WriteHeader(context);
                DumpHelper.WriteValue(element, context);
                return;
            }

            if (element is IEnumerable)
            {
                if (!context.EnumerateCollections)
                {
                    return;
                }

                // TODO: enumerate
            }

            var properties = elementType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var property in properties)
            {
                var newValue = property.GetValue(element);
                DumpHelper.Write(newValue, context.RecursionCopy(property.Name));
            }
        }

        /// <summary>
        ///     Determines if the specificed type is a simple primitive, enum or string
        /// </summary>
        /// <param name="typeToCheck">type to check</param>
        /// <returns>true if the typeToCheck is simple</returns>
        private static bool IsSimpleType(Type typeToCheck)
        {
            Arguments.NotNull(typeToCheck, nameof(typeToCheck));

            return typeToCheck.IsPrimitive || typeToCheck.IsEnum || typeToCheck == typeof(string);
        }

        /// <summary>
        ///     writes/walks the element's name/header into a context
        /// </summary>
        /// <param name="context">current dump context</param>
        private static void WriteHeader(DumperContext context)
        {
            Arguments.NotNull(context, nameof(context));

            context.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}:", new string(' ', context.CurrentDepth), context.FieldName);
        }

        /// <summary>
        ///     writes/walks the element's value into a context
        /// </summary>
        /// <param name="element">element to write/walk</param>
        /// <param name="context">current dump context</param>
        private static void WriteValue(object element, DumperContext context)
        {
            Arguments.NotNull(context, nameof(context));

            if (element == null)
            {
                context.Append("null");
            }
            else if (element is DateTime)
            {
                context.Append(((DateTime)element).ToShortDateString());
            }
            else if (element is char charElement)
            {
                if (charElement == '\0')
                {
                    context.Append("''");
                }
                else
                {
                    context.Append("'" + charElement + "'");
                }
            }
            else if (element is ValueType || element is string)
            {
                context.Append(element.ToString()!);
            }
            else if (element is IEnumerable)
            {
                context.Append("...");
            }
            else
            {
                context.Append("{ }");
            }

            context.Append(Environment.NewLine);
        }
    }
}