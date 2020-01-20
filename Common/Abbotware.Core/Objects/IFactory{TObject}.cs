// -----------------------------------------------------------------------
// <copyright file="IFactory{TObject}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    /// <summary>
    ///     factory Interface that can be used to dynamically create objets
    /// </summary>
    /// <typeparam name="TObject">type for factory to create</typeparam>
    public interface IFactory<TObject>
    {
        /// <summary>
        /// creates an object
        /// </summary>
        /// <returns>created object</returns>
        TObject Create();

        /// <summary>
        /// destroy / release the object
        /// </summary>
        /// <param name="object">object to release</param>
        void Destroy(TObject @object);
    }
}