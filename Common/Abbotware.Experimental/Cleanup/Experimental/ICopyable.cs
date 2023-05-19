// -----------------------------------------------------------------------
// <copyright file="ICopyable.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core
{
    /// <summary>
    ///     interface for copying an object
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    [Code(Quality.Experimental)]
    public interface ICopyable<out T>
    {
        /// <summary>
        ///     performs a copy
        /// </summary>
        /// <param name="copyType">type of copy</param>
        /// <returns>copied object</returns>
        T Copy(CopyType copyType);

        /// <summary>
        ///     performs a deep copy
        /// </summary>
        /// <returns>copied object</returns>
        T CopyDeep();

        /// <summary>
        ///     performs a shallow copy
        /// </summary>
        /// <returns>copied object</returns>
        T CopyShallow();
    }
}