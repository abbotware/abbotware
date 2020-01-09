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
            switch (sqlType.SqlDbType)
            {
                case SqlDbType.BigInt:
                    return typeof(long);
                case SqlDbType.DateTimeOffset:
                    return typeof(DateTimeOffset);
                case SqlDbType.DateTime:
                case SqlDbType.Date:
                    return typeof(DateTime);
                case SqlDbType.Decimal:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    return typeof(decimal);
                case SqlDbType.Int:
                    return typeof(int);
                case SqlDbType.TinyInt:
                    return typeof(byte);
                case SqlDbType.SmallInt:
                    return typeof(short);
                case SqlDbType.Float:
                    return typeof(double);
                case SqlDbType.VarChar:
                case SqlDbType.NVarChar:
                case SqlDbType.NChar:
                case SqlDbType.Char:
                case SqlDbType.Xml:
                    return typeof(string);
                case SqlDbType.UniqueIdentifier:
                    return typeof(Guid);
                case SqlDbType.Bit:
                    return typeof(bool);
                case SqlDbType.Binary:
                case SqlDbType.Image:
                case SqlDbType.VarBinary:
                    return typeof(byte[]);
                case SqlDbType.Timestamp:
                    return typeof(byte[]);

                default:
                    throw new InvalidOperationException(FormattableString.Invariant($"unexpected type:{sqlType.SqlDbType}"));
            }
        }
    }
}