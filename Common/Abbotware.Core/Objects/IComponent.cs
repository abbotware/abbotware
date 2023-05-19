// -----------------------------------------------------------------------
// <copyright file="IComponent.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    using System;

    /// <summary>
    /// Interface for a Component
    /// </summary>
    public interface IComponent : IInitializable, IDisposable
    {
    }

    /// <summary>
    /// Interface for a Component with configuration
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the configuration class</typeparam>
    public interface IComponent<out TConfiguration> : IComponent, IContainsConfiguration<TConfiguration>
        where TConfiguration : class
    {
    }
}