// -----------------------------------------------------------------------
// <copyright file="IFactory{TObject}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
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
        /// creates a resource
        /// </summary>
        /// <returns>created resource</returns>
        TObject Create();

        /// <summary>
        /// destroy / release the resource
        /// </summary>
        /// <param name="resource">resource to release</param>
        void Destroy(TObject resource);
    }
}