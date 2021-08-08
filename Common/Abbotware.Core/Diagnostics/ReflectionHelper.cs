// -----------------------------------------------------------------------
// <copyright file="ReflectionHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Diagnostics
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    ///     Helper class that performs reflection related tasks
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// checks if a PropertyInfo type is nullable value type
        /// </summary>
        /// <param name="propertyInfo">property info</param>
        /// <returns>true if nullable</returns>
        public static bool IsNullableValueType(PropertyInfo propertyInfo)
        {
            propertyInfo = Arguments.EnsureNotNull(propertyInfo, nameof(propertyInfo));

            var propertyType = propertyInfo.PropertyType;

            var nullable = propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>);

            return nullable;
        }

        /// <summary>
        /// Gets the type name of a PropertyInfo
        /// </summary>
        /// <param name="propertyInfo">property info</param>
        /// <returns>name of type</returns>
        public static string GetPropertyDataType(PropertyInfo propertyInfo)
        {
            propertyInfo = Arguments.EnsureNotNull(propertyInfo, nameof(propertyInfo));

            var propertyType = propertyInfo.PropertyType;

            string typeName;

            if (ReflectionHelper.IsNullableValueType(propertyInfo))
            {
                // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                typeName = propertyType.GetGenericArguments()[0].Name;
            }
            else
            {
                typeName = propertyType.Name;
            }

            return typeName;
        }

        /// <summary>
        ///     Gets an attribute from a class
        /// </summary>
        /// <typeparam name="TAttribute">attribute type</typeparam>
        /// <param name="type">class type </param>
        /// <returns>attribute if found</returns>
        public static TAttribute? SingleOrDefaultAttribute<TAttribute>(Type type)
        {
            type = Arguments.EnsureNotNull(type, nameof(type));

            return type.GetCustomAttributes(typeof(TAttribute), false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }

        /// <summary>
        ///     Gets multiple attributes from a property info
        /// </summary>
        /// <typeparam name="TAttribute">attribute type</typeparam>
        /// <param name="propertyInfo">Property type info </param>
        /// <returns>list of attributes if any found</returns>
        public static IEnumerable<TAttribute> Attributes<TAttribute>(MemberInfo propertyInfo)
        {
            propertyInfo = Arguments.EnsureNotNull(propertyInfo, nameof(propertyInfo));

            return propertyInfo.GetCustomAttributes(typeof(TAttribute), false)
                .OfType<TAttribute>();
        }

        /// <summary>
        ///     Gets an attribute from a property info
        /// </summary>
        /// <typeparam name="TAttribute">attribute type</typeparam>
        /// <param name="propertyInfo">Property type info </param>
        /// <returns>attribute if found</returns>
        public static TAttribute? SingleOrDefaultAttribute<TAttribute>(MemberInfo propertyInfo)
        {
            propertyInfo = Arguments.EnsureNotNull(propertyInfo, nameof(propertyInfo));

            return ReflectionHelper.Attributes<TAttribute>(propertyInfo)
                .SingleOrDefault();
        }

        /// <summary>
        /// Gets the property value if it is present on the object
        /// </summary>
        /// <typeparam name="T">property type</typeparam>
        /// <param name="source">object</param>
        /// <param name="propertyName">property name</param>
        /// <returns>value</returns>
        public static T? GetPropertyValueAsStruct<T>(object source, string propertyName)
            where T : struct
        {
            source = Arguments.EnsureNotNull(source, nameof(source));

            if (!TryGetPropertyValue(source, propertyName, out var val))
            {
                return null;
            }

            return (T?)val;
        }

        /// <summary>
        /// Gets the property value if it is present on the object
        /// </summary>
        /// <typeparam name="T">property type</typeparam>
        /// <param name="source">object</param>
        /// <param name="propertyName">property name</param>
        /// <returns>value</returns>
        public static T? GetPropertyValueAsClass<T>(object source, string propertyName)
            where T : class
        {
            source = Arguments.EnsureNotNull(source, nameof(source));

            if (!TryGetPropertyValue(source, propertyName, out var val))
            {
                return null;
            }

            return (T?)val;
        }

        /// <summary>
        ///     Gets the properties of a class
        /// </summary>
        /// <typeparam name="TClass">class type</typeparam>
        /// <param name="bindings">optional binding flags (default is public)</param>
        /// <returns>list of property names</returns>
        public static IEnumerable<string> PropertyNames<TClass>(BindingFlags bindings)
        {
            var properties = typeof(TClass).GetProperties(bindings);

            return properties.Select(x => x.Name).ToList();
        }

        /// <summary>
        ///     Gets the properties of a class
        /// </summary>
        /// <typeparam name="TClass">class type</typeparam>
        /// <returns>list of property names</returns>
        public static IEnumerable<string> PropertyNames<TClass>()
        {
            return ReflectionHelper.PropertyNames<TClass>(BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        ///     Gets the properties of a class
        /// </summary>
        /// <typeparam name="TClass">class type</typeparam>
        /// <returns>list of property info</returns>
        public static IEnumerable<PropertyInfo> Properties<TClass>()
        {
            return ReflectionHelper.Properties<TClass>(BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        ///     Gets the property values of a class
        /// </summary>
        /// <typeparam name="TClass">class type</typeparam>
        /// <param name="instance">instance of class to flatten</param>
        /// <returns>list of property info</returns>
        public static IReadOnlyDictionary<string, object?> Flatten<TClass>(TClass instance)
        {
            return Flatten(instance, Enumerable.Empty<string>());
        }

        /// <summary>
        ///     Gets the property values of a class
        /// </summary>
        /// <typeparam name="TClass">class type</typeparam>
        /// <param name="instance">instance of class to flatten</param>
        /// <param name="ignore">list of properties to ignore</param>
        /// <returns>list of property info</returns>
        public static IReadOnlyDictionary<string, object?> Flatten<TClass>(TClass instance, IEnumerable<string> ignore)
        {
            var values = new Dictionary<string, object?>();

            var ignoreList = ignore.ToList();

            var props = Properties<TClass>(BindingFlags.Public | BindingFlags.Instance);

            foreach (var p in props)
            {
                if (ignoreList.Contains(p.Name))
                {
                    continue;
                }

                if (instance == null)
                {
                    values.Add(p.Name, null);
                    continue;
                }

                values.Add(p.Name, p.GetValue(instance));
            }

            return values;
        }

        /// <summary>
        ///     Gets the properties of a class
        /// </summary>
        /// <typeparam name="TClass">class type</typeparam>
        /// <returns>list of property names</returns>
        public static IEnumerable<string> GetSimplePropertyNames<TClass>()
        {
            var properties = ReflectionHelper.GetSimpleProperties<TClass>();

            return properties.Select(x => x.Name).ToList();
        }

        /// <summary>
        ///     Gets the 'simple' (intrinsic types + string) properties of a class
        /// </summary>
        /// <typeparam name="TClass">class type</typeparam>
        /// <returns>list of property info</returns>
        public static IEnumerable<PropertyInfo> GetSimpleProperties<TClass>()
        {
            var properties = typeof(TClass).GetProperties();

            var nullablePropertyInfos = properties
                .Where(x => Nullable.GetUnderlyingType(x.PropertyType) != null)
                .ToList();

            nullablePropertyInfos = nullablePropertyInfos
                .Where(x => Nullable.GetUnderlyingType(x.PropertyType)!.IsPrimitive
                        || (Nullable.GetUnderlyingType(x.PropertyType) == typeof(decimal))
                        || (Nullable.GetUnderlyingType(x.PropertyType) == typeof(DateTime))
                        || (Nullable.GetUnderlyingType(x.PropertyType) == typeof(DateTimeOffset)))
                .ToList();

            var propertyInfos = properties
                .Where(x => x.PropertyType.IsPrimitive
                        || (x.PropertyType == typeof(decimal))
                        || (x.PropertyType == typeof(string))
                        || (x.PropertyType == typeof(DateTime))
                        || (x.PropertyType == typeof(DateTimeOffset))
                        || (x.PropertyType == typeof(Uri)))
                .ToList();

            return nullablePropertyInfos.Union(propertyInfos).ToList();
        }

        /// <summary>
        ///     Gets the properties of a class
        /// </summary>
        /// <typeparam name="TClass">class type</typeparam>
        /// <param name="bindings">optional binding flags (default is public)</param>
        /// <returns>list of property names</returns>
        public static IEnumerable<PropertyInfo> Properties<TClass>(BindingFlags bindings)
        {
            var t = typeof(TClass);

            return t.GetProperties(bindings)
                .ToList();
        }

        /// <summary>
        ///     Compares the properties of two objects of the same type.  Throws exception if not equal
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compre.</param>
        /// <param name="ignoreList">A list of property names to ignore from the comparison.</param>
        public static void ThrowIfNotEqual(object? left, object? right, params string[] ignoreList)
        {
            AreObjectsEqual(left, right, true, ignoreList);
        }

        /// <summary>
        ///     Compares the properties of two objects of the same type and returns if all properties are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compre.</param>
        /// <param name="ignoreList">A list of property names to ignore from the comparison.</param>
        /// <returns><c>true</c> if all property values are equal, otherwise <c>false</c>.</returns>
        public static bool AreObjectsEqual(object? left, object? right, params string[] ignoreList)
        {
            return AreObjectsEqual(left, right, false, ignoreList);
        }

        /// <summary>
        ///     Compares the properties of two objects of the same type and returns if all properties are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compre.</param>
        /// <param name="useException">throw exception with message</param>
        /// <param name="ignoreList">A list of property names to ignore from the comparison.</param>
        /// <returns><c>true</c> if all property values are equal, otherwise <c>false</c>.</returns>
        public static bool AreObjectsEqual(object? left, object? right, bool useException, params string[] ignoreList)
        {
            bool result;

            if ((left != null) && (right != null))
            {
                var objectType = left.GetType();

                result = true; // assume by default they are equal

                foreach (var propertyInfo in objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && !ignoreList.Contains(p.Name)))
                {
                    var valueA = propertyInfo.GetValue(left, null);
                    var valueB = propertyInfo.GetValue(right, null);

                    //// if it is a primative type, value type or implements IComparable, just directly try and compare the value
                    if (ReflectionHelper.CanDirectlyCompare(propertyInfo.PropertyType))
                    {
                        if (!ReflectionHelper.AreValuesEqual(valueA, valueB))
                        {
                            DebugMessage(useException, "Mismatch with property '{0}.{1}' found.", objectType.FullName, propertyInfo.Name);
                            result = false;
                        }
                    }
                    else if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
                    {
                        //// if it implements IEnumerable, then scan any items

                        // null check
                        if ((valueA == null) && (valueB == null))
                        {
                            continue;
                        }
                        else if (((valueA == null) && (valueB != null)) || ((valueA != null) && (valueB == null)))
                        {
                            DebugMessage(useException, "Mismatch with property '{0}.{1}' found.", objectType.FullName, propertyInfo.Name);
                            result = false;
                        }
                        else
                        {
                            var collectionItems1 = ((IEnumerable?)valueA!).Cast<object>();
                            var collectionItems2 = ((IEnumerable?)valueB!).Cast<object>();
                            var collectionItemsCount1 = collectionItems1.Count();
                            var collectionItemsCount2 = collectionItems2.Count();

                            // check the counts to ensure they match
                            if (collectionItemsCount1 != collectionItemsCount2)
                            {
                                DebugMessage(useException, "Collection counts for property '{0}.{1}' do not match.", objectType.FullName, propertyInfo.Name);
                                result = false;
                            }
                            else
                            {
                                // and if they do, compare each item... this assumes both collections have the same order
                                for (var i = 0; i < collectionItemsCount1; i++)
                                {
                                    var collectionItem1 = collectionItems1.ElementAt(i);
                                    var collectionItem2 = collectionItems2.ElementAt(i);

                                    var collectionItemType = collectionItem1.GetType();

                                    if (ReflectionHelper.CanDirectlyCompare(collectionItemType))
                                    {
                                        if (!ReflectionHelper.AreValuesEqual(collectionItem1, collectionItem2))
                                        {
                                            DebugMessage(useException, "Item {0} in property collection '{1}.{2}' does not match.", i, objectType.FullName, propertyInfo.Name);
                                            result = false;
                                        }
                                    }
                                    else if (!ReflectionHelper.AreObjectsEqual(collectionItem1, collectionItem2, useException, ignoreList))
                                    {
                                        DebugMessage(useException, "Item {0} in property collection '{1}.{2}' does not match.", i, objectType.FullName, propertyInfo.Name);
                                        result = false;
                                    }
                                }
                            }
                        }
                    }
                    else if (propertyInfo.PropertyType.IsClass)
                    {
                        if (!ReflectionHelper.AreObjectsEqual(propertyInfo.GetValue(left, null), propertyInfo.GetValue(right, null), useException, ignoreList))
                        {
                            DebugMessage(useException, "Mismatch with property '{0}.{1}' found.", objectType.FullName, propertyInfo.Name);
                            result = false;
                        }
                    }
                    else
                    {
                        DebugMessage(useException, "Cannot compare property '{0}.{1}'.", objectType.FullName, propertyInfo.Name);
                        result = false;
                    }
                }
            }
            else
            {
                result = object.Equals(left, right);
            }

            return result;
        }

        private static void DebugMessage(bool useException, string message, params object?[] args)
        {
            var msg = string.Format(CultureInfo.InvariantCulture, message, args);

            if (useException)
            {
                throw new ArgumentException(msg);
            }

            Debug.WriteLine(msg);
        }

        /// <summary>
        ///     Determines whether value instances of the specified type can be directly compared.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///     <c>true</c> if this value instances of the specified type can be directly compared; otherwise, <c>false</c>.
        /// </returns>
        private static bool CanDirectlyCompare(Type type)
        {
            Arguments.NotNull(type, nameof(type));

            if (type.IsValueType && type.IsGenericType)
            {
                if (type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
                {
                    return false;
                }
            }

            return typeof(IComparable).IsAssignableFrom(type) || type.IsPrimitive || type.IsValueType;
        }

        /// <summary>
        ///     Compares two values and returns if they are the same.
        /// </summary>
        /// <param name="valueA">The first value to compare.</param>
        /// <param name="valueB">The second value to compare.</param>
        /// <returns><c>true</c> if both values match, otherwise <c>false</c>.</returns>
        private static bool AreValuesEqual(object? valueA, object? valueB)
        {
            bool result;

            if (((valueA == null) && (valueB != null)) || ((valueA != null) && (valueB == null)))
            {
                result = false; // one of the values is null
            }
            else if ((valueA is IComparable selfValueComparer) && (selfValueComparer.CompareTo(valueB) != 0))
            {
                result = false; // the comparison using IComparable failed
            }
            else if (!object.Equals(valueA, valueB))
            {
                result = false; // the comparison using Equals failed
            }
            else
            {
                result = true; // match
            }

            return result;
        }

        private static bool TryGetPropertyValue(object source, string propertyName, out object? value)
        {
            value = null;

            var property = source.GetType()
                .GetProperty(propertyName);

            if (property == null)
            {
                return false;
            }

            value = property.GetValue(source, null);

            if (value == null)
            {
                return false;
            }

            return true;
        }
    }
}