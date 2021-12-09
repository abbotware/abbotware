// -----------------------------------------------------------------------
// <copyright file="IConfigurationValues.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.ExtensionPoints
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;

    /// <summary>
    ///     interface for reading configuration values from the configuration store
    /// </summary>
    public interface IConfigurationValues
    {
        /// <summary>
        ///     Gets the id of the loaded configuraiton
        /// </summary>
        long Id { get; }

        /// <summary>
        ///     Gets the name of the loaded configuration
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets the attribute id for a given attribute
        /// </summary>
        /// <param name="attribute">name of attribute</param>
        /// <returns>id of attribute</returns>
        long this[string attribute] { get; }

#pragma warning disable CA1720 // Identifier contains type name
        /// <summary>
        ///     Gets the value of Boolean attribute
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <returns>value of the attribute</returns>
        bool Bool(string attribute);

        /// <summary>
        ///     Gets the value of Boolean attribute
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <returns>value of the attribute</returns>
        bool Bool(long attributeId);

        /// <summary>
        ///     Gets the value of UInt16 (ushort) attribute
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <returns>value of the attribute</returns>
        ushort UInt16(string attribute);

        /// <summary>
        ///     Gets the value of UInt16 (ushort) attribute
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <returns>value of the attribute</returns>
        ushort UInt16(long attributeId);

        /// <summary>
        ///     Gets the value of UInt32 (uint) attribute
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <returns>value of the attribute</returns>
        uint UInt32(string attribute);

        /// <summary>
        ///     Gets the value of UInt32 (uint) attribute
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <returns>value of the attribute</returns>
        uint UInt32(long attributeId);

        /// <summary>
        ///     Gets the value of UInt32 (uint?) attribute or Null if it is not set
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <returns>value of the attribute</returns>
        uint? UInt32OrNull(string attribute);

        /// <summary>
        ///     Gets the value of UInt32 (uint?) attribute or Null if it is not set
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <returns>value of the attribute</returns>
        uint? UInt32OrNull(long attributeId);

        /// <summary>
        ///     Gets the value of Int64 (long) attribute
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <returns>value of the attribute</returns>
        long Int64(string attribute);

        /// <summary>
        ///     Gets the value of Int64 (long) attribute
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <returns>value of the attribute</returns>
        long Int64(long attributeId);

        /// <summary>
        ///     Gets the value of Int32 (int) attribute
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <returns>value of the attribute</returns>
        int Int32(string attribute);

        /// <summary>
        ///     Gets the value of Int32 (int) attribute
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <returns>value of the attribute</returns>
        int Int32(long attributeId);

        /// <summary>
        ///     Gets the value of Int32 (int?) attribute or Null if it is not set
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <returns>value of the attribute</returns>
        int? Int32OrNull(string attribute);

        /// <summary>
        ///     gets the value of Int32 (int?) attribute or Null if it is not set
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <returns>value of the attribute</returns>
        int? Int32OrNull(long attributeId);

        /// <summary>
        ///     Gets the value of TimeSpan attribute
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <returns>value of the attribute</returns>
        TimeSpan TimeSpan(string attribute);

        /// <summary>
        ///     Gets the value of TimeSpan attribute
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <returns>value of the attribute</returns>
        TimeSpan TimeSpan(long attributeId);

        /// <summary>
        ///     Gets the value of DateTime attribute
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <returns>value of the attribute</returns>
        DateTime DateTime(string attribute);

        /// <summary>
        ///     Gets the value of DateTime attribute
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <returns>value of the attribute</returns>
        DateTime DateTime(long attributeId);

        /// <summary>
        ///     Gets the value of Decimal attribute
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <returns>value of the attribute</returns>
        decimal Decimal(string attribute);

        /// <summary>
        ///     Gets the value of Decimal attribute
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <returns>value of the attribute</returns>
        decimal Decimal(long attributeId);

        /// <summary>
        ///     Gets the value of Double attribute
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <returns>value of the attribute</returns>
        double Double(string attribute);

        /// <summary>
        ///     Gets the value of Double attribute
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <returns>value of the attribute</returns>
        double Double(long attributeId);

        /// <summary>
        ///     Gets the value of Double attribute or Null if it is not set
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <returns>value of the attribute</returns>
        double? DoubleOrNull(long attributeId);

        /// <summary>
        ///     Gets the value of String attribute
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <returns>value of the attribute</returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "String", Justification = "this is intended")]
        string String(string attribute);

        /// <summary>
        ///     Gets the value of String attribute
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <returns>value of the attribute</returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "String", Justification = "this is intended")]
        string String(long attributeId);

        /// <summary>
        ///     Gets the value of String attribute, can be null
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <returns>value of the attribute</returns>
        string StringOrNull(long attributeId);
#pragma warning restore CA1720 // Identifier contains type name

        /// <summary>
        ///     Gets the value of NetworkCredential attribute
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <returns>value of the attribute</returns>
        NetworkCredential NetworkCredential(string attribute);

        /// <summary>
        ///     Gets the value of NetworkCredential attribute
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <returns>value of the attribute</returns>
        NetworkCredential NetworkCredential(long attributeId);
    }
}