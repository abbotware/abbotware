// -----------------------------------------------------------------------
// <copyright file="InheritedTypeInstaller.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Castle.ExtensionPoints
{
    using Abbotware.Core;
    using global::Castle.MicroKernel.Registration;

    /// <summary>
    ///     Abstract class for registering types in an assembly based on a specific type T
    /// </summary>
    /// <typeparam name="T">base type to use for searching</typeparam>
    public abstract class InheritedTypeInstaller<T> : BaseTypeInstaller
    {
        /// <inheritdoc />
        protected override BasedOnDescriptor OnFindTypes()
        {
            return Classes.FromAssemblyContaining<T>()
                .BasedOn<T>();
        }
    }
}