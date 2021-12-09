// -----------------------------------------------------------------------
// <copyright file="SqlTypeMapping.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Data.Schema
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Reflection;
    using Abbotware.Core;
    using Abbotware.Core.Diagnostics;
    using Microsoft.Data.SqlClient.Server;

    /// <summary>
    /// Helper class used for C# to SQL type mapping
    /// </summary>
    public static class SqlTypeMapping
    {
        /// <summary>
        /// checks the supplied PropertyInfo if it is a supported type
        /// </summary>
        /// <param name="property">property info</param>
        /// <returns>true/false if supported or not</returns>
        public static bool SupportedCSharpType(PropertyInfo property)
        {
            property = Arguments.EnsureNotNull(property, nameof(property));

            var actualType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

            if (actualType.IsPrimitive)
            {
                return true;
            }

            if (actualType == typeof(string))
            {
                return true;
            }

            if (actualType == typeof(decimal))
            {
                return true;
            }

            if (actualType == typeof(Guid))
            {
                return true;
            }

            if (actualType == typeof(DateTime))
            {
                return true;
            }

            if (actualType == typeof(DateTimeOffset))
            {
                return true;
            }

            if (actualType == typeof(byte[]))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// checks if sql type is compatible with class property type
        /// </summary>
        /// <param name="sqlType">sql type</param>
        /// <param name="property">class property</param>
        /// <returns>true if types are compatible</returns>
        public static bool AreTypesCompatible(SqlMetaData sqlType, PropertyInfo property)
        {
            sqlType = Arguments.EnsureNotNull(sqlType, nameof(sqlType));
            property = Arguments.EnsureNotNull(property, nameof(property));

            var actualType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

            if (property.PropertyType == typeof(string))
            {
                var maxLength = ReflectionHelper.SingleOrDefaultAttribute<MaxLengthAttribute>(property)?.Length ?? -1;
                maxLength = ReflectionHelper.SingleOrDefaultAttribute<StringLengthAttribute>(property)?.MaximumLength ?? maxLength;

                if (sqlType.MaxLength != maxLength)
                {
                    return false;
                }
            }

            return actualType == SqlTypeMapping.SqlToCSharp(sqlType);
        }

        /// <summary>
        /// converts a sql type to a .net type
        /// </summary>
        /// <param name="sqlType">sql type</param>
        /// <returns>.net type</returns>
        public static Type SqlToCSharp(SqlMetaData sqlType)
        {
            sqlType = Arguments.EnsureNotNull(sqlType, nameof(sqlType));

            // Based off the following document
            // https://msdn.microsoft.com/en-us/library/cc716729(v=vs.110).aspx
            return sqlType.SqlDbType switch
            {
                SqlDbType.BigInt => typeof(long),
                SqlDbType.DateTimeOffset => typeof(DateTimeOffset),
                SqlDbType.DateTime or SqlDbType.Date => typeof(DateTime),
                SqlDbType.Decimal or SqlDbType.Money or SqlDbType.SmallMoney => typeof(decimal),
                SqlDbType.Int => typeof(int),
                SqlDbType.TinyInt => typeof(byte),
                SqlDbType.SmallInt => typeof(short),
                SqlDbType.Float => typeof(double),
                SqlDbType.VarChar or SqlDbType.NVarChar or SqlDbType.NChar or SqlDbType.Char or SqlDbType.Xml => typeof(string),
                SqlDbType.UniqueIdentifier => typeof(Guid),
                SqlDbType.Bit => typeof(bool),
                SqlDbType.Binary or SqlDbType.Image or SqlDbType.VarBinary => typeof(byte[]),
                SqlDbType.Timestamp => typeof(byte[]),
                _ => throw new InvalidOperationException(FormattableString.Invariant($"unexpected type:{sqlType.SqlDbType}")),
            };
        }
    }
}