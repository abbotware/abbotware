// -----------------------------------------------------------------------
// <copyright file="IAsyncComponent.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    /// <summary>
    /// Interface for a Component
    /// </summary>
    public interface IAsyncComponent : IComponent, IAsyncInitializable
    {
    }
}