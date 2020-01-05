// -----------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions
{
    using System;
    using System.Text;

    /// <summary>
    ///     Extension methods dealing with Type objects
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        ///     Gets a friendly name of the type
        /// </summary>
        /// <param name="type">instance of Type object being extended</param>
        /// <returns>name of class</returns>
        public static string GetFriendlyName(this Type type)
        {
            var t = Arguments.EnsureNotNull(type, nameof(type));

            if (!t.IsGenericType)
            {
                return t.GetNestedTypeName();
            }

            var stringBuilder = new StringBuilder();

            BuildClassNameRecursively(t, stringBuilder);

            return stringBuilder.ToString();
        }

        /// <summary>
        ///     Internal helper for GetFriendlyName function
        /// </summary>
        /// <param name="type">instance of Type object being recursively walked</param>
        /// <param name="classNameBuilder">string builder containing name shared among all recursive calls</param>
        /// <param name="genericParameterIndex">generic parameter index used in recusrve calls</param>
        private static void BuildClassNameRecursively(Type type, StringBuilder classNameBuilder, uint genericParameterIndex = 0)
        {
            var t = Arguments.EnsureNotNull(type, nameof(type));
            var cnb = Arguments.EnsureNotNull(classNameBuilder, nameof(classNameBuilder));

            if (t.IsGenericParameter)
            {
                cnb.Append(FormattableString.Invariant($"T{genericParameterIndex + 1}"));
            }
            else if (t.IsGenericType)
            {
                cnb.Append(t.GetNestedTypeName() + "[");

                var subIndex = 0U;

                foreach (var genericTypeArgument in t.GetGenericArguments())
                {
                    if (subIndex > 0)
                    {
                        cnb.Append(",");
                    }

                    BuildClassNameRecursively(genericTypeArgument, cnb, subIndex++);
                }

                cnb.Append("]");
            }
            else
            {
                cnb.Append(t.GetNestedTypeName());
            }
        }

        /// <summary>
        ///     Internal helper for GetFriendlyName function
        /// </summary>
        /// <param name="type">instance of Type object being recursively walked</param>
        /// <returns>nested type name</returns>
        private static string GetNestedTypeName(this Type type)
        {
            var t = Arguments.EnsureNotNull(type, nameof(type));

            if (!t.IsNested)
            {
                return t.Name;
            }

            var nestedName = new StringBuilder();

            while (t != null)
            {
                if (nestedName.Length > 0)
                {
                    nestedName.Insert(0, '.');
                }

                var temp = t.IsGenericType ? t.Name.Split('`')[0] : t.Name;

                nestedName.Insert(0, temp);

                t = t.DeclaringType;
            }

            return nestedName.ToString();
        }
    }
}