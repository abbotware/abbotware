// -----------------------------------------------------------------------
// <copyright file="IContainsConfiguration{TConfiguration}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2019. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    /// <summary>
    ///     Interface that implements a object with configuration
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the configuration class</typeparam>
    public interface IContainsConfiguration<out TConfiguration>
        where TConfiguration : class
    {
        /// <summary>
        ///     Gets the configuration object for the manager class
        /// </summary>
        TConfiguration Configuration { get; }
    }
}