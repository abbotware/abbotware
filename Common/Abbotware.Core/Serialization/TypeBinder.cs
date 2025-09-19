// -----------------------------------------------------------------------
// <copyright file="TypeBinder.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Serialization
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Specific Type Binder for serialization
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="TypeBinder"/> class.
    /// </remarks>
    /// <param name="expectedType">expected type for binding</param>
    public class TypeBinder(Type expectedType) : SerializationBinder
    {
        /// <inheritdoc/>
        public override Type BindToType(string assemblyName, string typeName)
        {
            if (expectedType.Assembly.FullName == assemblyName && expectedType.Name == typeName)
            {
                return expectedType;
            }

            throw new ArgumentException("Unexpected type", nameof(typeName));
        }
    }
}