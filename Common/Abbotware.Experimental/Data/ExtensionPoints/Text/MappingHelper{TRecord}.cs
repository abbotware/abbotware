// -----------------------------------------------------------------------
// <copyright file="MappingHelper{TRecord}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints.Text
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Abbotware.Core.Diagnostics;

    /// <summary>
    ///     class that helps map the parser header fields to class properties
    /// </summary>
    /// <typeparam name="TRecord">Type of the class</typeparam>
    public class MappingHelper<TRecord>
        where TRecord : new()
    {
        /// <summary>
        ///     flag indicating whether or not its ok for the class to have unmapped properties during the validation
        /// </summary>
        private readonly bool allowClassToHaveExtraProperties;

        /// <summary>
        ///     flag indicating whether or not its ok for the file to have unmapped properties during the validation
        /// </summary>
        private readonly bool allowFileToHaveExtraProperties;

        /// <summary>
        ///     map of header to property info
        /// </summary>
        private readonly Dictionary<string, PropertyInfo> headerToProperty;

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingHelper{TRecord}"/> class.
        /// </summary>
        public MappingHelper()
            : this(false, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingHelper{TRecord}"/> class.
        /// </summary>
        /// <param name="allowFileToHaveExtraProperties">configs verification to allow file to have extra fields</param>
        /// <param name="allowClassToHaveExtraProperties">configures verification to allow class to have extra properties</param>
        public MappingHelper(bool allowFileToHaveExtraProperties, bool allowClassToHaveExtraProperties)
        {
            this.allowFileToHaveExtraProperties = allowFileToHaveExtraProperties;
            this.allowClassToHaveExtraProperties = allowClassToHaveExtraProperties;
            this.headerToProperty = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Gets the class property for a field header
        /// </summary>
        /// <param name="fieldHeader">name of the field header</param>
        /// <returns>cached property info</returns>
        public PropertyInfo this[string fieldHeader]
        {
            get { return this.headerToProperty[fieldHeader]; }
        }

        /// <summary>
        /// Checks if the field header is mapped
        /// </summary>
        /// <param name="fieldHeader">name of the field header</param>
        /// <returns>true if the header is mapped</returns>
        public bool ContainsHeader(string fieldHeader)
        {
            return this.headerToProperty.ContainsKey(fieldHeader);
        }

        /// <summary>
        /// verifies parser fields - class properties
        /// </summary>
        /// <param name="parser">parser</param>
        public void VerifyHeaders(IFieldBasedParser<TRecord> parser)
        {
            Arguments.NotNull(parser, nameof(parser));

            // Verify Class:
            // For each property of TModel check if class properties has corresponding csv fields
            // save  header -> propertyInfo  in dictionary
            var classProperties = typeof(TRecord).GetProperties();

            var fileHeaders = parser.FieldHeaders().Select(m => m.Trim('|').ToUpperInvariant()).ToList();

            var extraProperties = new List<string>();

            foreach (var property in classProperties)
            {
                var normalizedName = property.Name.ToUpperInvariant();

                var aliases = ReflectionHelper.Attributes<MappingAliasAttribute>(property);

                if (fileHeaders.Contains(normalizedName))
                {
                    this.headerToProperty.Add(normalizedName, property);
                }
                else if (aliases != null)
                {
                    foreach (MappingAliasAttribute alias in aliases)
                    {
                        var normalizedAlias = alias.AlternateName.ToUpperInvariant();
                        if (fileHeaders.Contains(normalizedAlias))
                        {
                            this.headerToProperty.Add(normalizedAlias, property);
                            break;
                        }
                    }
                }
                else
                {
                    extraProperties.Add(property.Name);
                }
            }

            // Verify Header:
            // For each header, check if parser fields have matching class properties:
            // check if header in dictionary
            var extraFileHeaders = new List<string>();

            foreach (var header in fileHeaders)
            {
                if (!this.headerToProperty.ContainsKey(header))
                {
                    extraFileHeaders.Add(header);
                }
            }

            if (extraFileHeaders.Count + extraProperties.Count > 0)
            {
                var message = string.Format(
                    CultureInfo.InvariantCulture,
                    "headers absent in properties: {0}\n properties absent in headers: {1}",
                    string.Join(", ", extraFileHeaders.ToArray()),
                    string.Join(", ", extraProperties.ToArray()));

                var stopFlag = (!this.allowFileToHaveExtraProperties && extraFileHeaders.Count != 0) || (!this.allowClassToHaveExtraProperties && extraProperties.Count != 0);

                if (stopFlag)
                {
                    throw new InvalidOperationException(message);
                }
            }
        }
    }
}