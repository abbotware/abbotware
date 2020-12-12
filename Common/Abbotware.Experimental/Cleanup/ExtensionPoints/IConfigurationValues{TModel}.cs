// -----------------------------------------------------------------------
// <copyright file="IConfigurationValues{TModel}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.ExtensionPoints
{
    using System;

    /// <summary>
    ///     interface for reading configuration values from the configuration store using an object for rule / logic evaluation
    /// </summary>
    /// <typeparam name="TModel">type of model used as a rule engine parameter</typeparam>
    public interface IConfigurationValues<in TModel> : IConfigurationValues
    {
#pragma warning disable CA1720 // Identifier contains type name
        /// <summary>
        ///     Gets the value of Boolean attribute using the model
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <param name="model">model instance</param>
        /// <returns>value of the attribute</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "bool", Justification = "this is intended")]
        bool Bool(string attribute, TModel model);

        /// <summary>
        ///     Gets the value of Boolean attribute using the model
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <param name="model">model instance</param>
        /// <returns>value of the attribute</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "bool", Justification = "this is intended")]
        bool Bool(long attributeId, TModel model);

        /// <summary>
        ///     Gets the value of UInt16 (ushort) attribute using the model
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <param name="model">model instance</param>
        /// <returns>value of the attribute</returns>
        ushort UInt16(string attribute, TModel model);

        /// <summary>
        ///     Gets the value of UInt16 (ushort) attribute using the model
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <param name="model">model instance</param>
        /// <returns>value of the attribute</returns>
        ushort UInt16(long attributeId, TModel model);

        /// <summary>
        ///     Gets the value of Int64 (long) attribute using the model
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <param name="model">model instance</param>
        /// <returns>value of the attribute</returns>
        long Int64(string attribute, TModel model);

        /// <summary>
        ///     Gets the value of Int64 (long) attribute using the model
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <param name="model">model instance</param>
        /// <returns>value of the attribute</returns>
        long Int64(long attributeId, TModel model);

        /// <summary>
        ///     Gets the value of DateTime attribute using the model
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <param name="model">model instance</param>
        /// <returns>value of the attribute</returns>
        DateTime DateTime(string attribute, TModel model);

        /// <summary>
        ///     Gets the value of DateTime attribute using the model
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <param name="model">model instance</param>
        /// <returns>value of the attribute</returns>
        DateTime DateTime(long attributeId, TModel model);

        /// <summary>
        ///     Gets the value of Decimal attribute using the model
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <param name="model">model instance</param>
        /// <returns>value of the attribute</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Decimal", Justification = "this is intended")]
        decimal Decimal(string attribute, TModel model);

        /// <summary>
        ///     Gets the value of Decimal attribute using the model
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <param name="model">model instance</param>
        /// <returns>value of the attribute</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Decimal", Justification = "this is intended")]
        decimal Decimal(long attributeId, TModel model);

        /// <summary>
        ///     Gets the value of Double attribute using the model
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <param name="model">model instance</param>
        /// <returns>value of the attribute</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Double", Justification = "this is intended")]
        double Double(string attribute, TModel model);

        /// <summary>
        ///     Gets the value of Double attribute using the model
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <param name="model">model instance</param>
        /// <returns>value of the attribute</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Double", Justification = "this is intended")]
        double Double(long attributeId, TModel model);

        /// <summary>
        ///     Gets the value of Double attribute using the model or Null if it is not set
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <param name="model">model instance</param>
        /// <returns>value of the attribute</returns>
        double? DoubleOrNull(string attribute, TModel model);

        /// <summary>
        ///     Gets the value of Double attribute using the model or Null if it is not set
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <param name="model">model instance</param>
        /// <returns>value of the attribute</returns>
        double? DoubleOrNull(long attributeId, TModel model);

        /// <summary>
        ///     Gets the value of String attribute using the model
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <param name="model">model instance</param>
        /// <returns>value of the attribute</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "String", Justification = "this is intended")]
        string String(string attribute, TModel model);

        /// <summary>
        ///     Gets the value of String attribute using the model
        /// </summary>
        /// <param name="attributeId">attribute id</param>
        /// <param name="model">model instance</param>
        /// <returns>value of the attribute</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "String", Justification = "this is intended")]
        string String(long attributeId, TModel model);
#pragma warning restore CA1720 // Identifier contains type name

    }
}