// -----------------------------------------------------------------------
// <copyright file="TypeBinder{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Serialization
{
    /// <summary>
    /// Specific Type Binder for serialization
    /// </summary>
    /// <typeparam name="T">type for binding</typeparam>
    public class TypeBinder<T> : TypeBinder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeBinder{T}"/> class.
        /// </summary>
        public TypeBinder()
            : base(typeof(T))
        {
        }
    }
}