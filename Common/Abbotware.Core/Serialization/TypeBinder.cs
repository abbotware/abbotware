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
    public class TypeBinder : SerializationBinder
    {
        private readonly Type expectedType;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeBinder"/> class.
        /// </summary>
        /// <param name="expectedType">expected type for binding</param>
        public TypeBinder(Type expectedType)
        {
            this.expectedType = expectedType;
        }

        /// <inheritdoc/>
        public override Type BindToType(string assemblyName, string typeName)
        {
            if (this.expectedType.Assembly.FullName == assemblyName && this.expectedType.Name == typeName)
            {
                return this.expectedType;
            }

            throw new ArgumentException("Unexpected type", nameof(typeName));
        }
    }
}