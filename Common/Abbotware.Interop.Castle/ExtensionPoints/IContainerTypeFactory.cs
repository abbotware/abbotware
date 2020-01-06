// -----------------------------------------------------------------------
// <copyright file="IContainerTypeFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Castle.ExtensionPoints
{
    using System;

    /// <summary>
    ///     interface for creating types via Type name or Generics
    /// </summary>
    public interface IContainerTypeFactory
    {
        /// <summary>
        ///     Creates a type from the container
        /// </summary>
        /// <param name="type">.net type name</param>
        /// <returns>object of type</returns>
        object Create(Type type);

        /// <summary>
        ///     Creates a type from the container
        /// </summary>
        /// <typeparam name="TObject">Type to create</typeparam>
        /// <returns>new TObjecto</returns>
        TObject Create<TObject>();
    }
}