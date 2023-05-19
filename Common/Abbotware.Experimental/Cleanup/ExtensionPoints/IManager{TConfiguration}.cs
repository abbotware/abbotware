// -----------------------------------------------------------------------
// <copyright file="IManager{TConfiguration}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.ExtensionPoints
{
    /// <summary>
    ///     Interface that implements a manager object
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the configuration class</typeparam>
    public interface IManager<out TConfiguration>
        where TConfiguration : class
    {
        /// <summary>
        ///     Gets the configuration object for the manager class
        /// </summary>
        TConfiguration Configuration { get; }
    }
}