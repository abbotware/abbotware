// -----------------------------------------------------------------------
// <copyright file="IRecyclable.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.ExtensionPoints
{
    /// <summary>
    /// interface that indicates an object can be recycled and reused
    /// </summary>
    public interface IRecyclable
    {
        /// <summary>
        /// recylces the object
        /// </summary>
        void Recycle();
    }
}