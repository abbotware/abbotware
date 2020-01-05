// -----------------------------------------------------------------------
// <copyright file="IInitializable.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    /// <summary>
    /// Interface for an initializable object
    /// </summary>
    public interface IInitializable
    {
        /// <summary>
        ///     Gets a value indicating whether or not object initialization has occurred
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// Explicitily performs the object initialization
        /// </summary>
        void Initialize();
    }
}