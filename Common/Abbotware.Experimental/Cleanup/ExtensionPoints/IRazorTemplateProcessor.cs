// -----------------------------------------------------------------------
// <copyright file="IRazorTemplateProcessor.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.ExtensionPoints
{
    using System;

    /// <summary>
    ///     Interface for binding razor string templates to objects
    /// </summary>
    public interface IRazorTemplateProcessor
    {
        /// <summary>
        ///     Creates a string template bound to the model
        /// </summary>
        /// <typeparam name="TModel">Type of model class to use for binding</typeparam>
        /// <param name="templateContent">template to use for view</param>
        /// <param name="model">model object to bind to view</param>
        /// <returns>string containing the model/view template evaluated</returns>
        string Build<TModel>(string templateContent, TModel model);

        /// <summary>
        ///     Creates a string template bound to the model
        /// </summary>
        /// <typeparam name="TModel">Type of model class to use for binding</typeparam>
        /// <param name="templateContent">template to use for view</param>
        /// <param name="templateCacheKey">key to cache template when compiling</param>
        /// <param name="model">model object to bind to view</param>
        /// <returns>string containing the model/view template evaluated</returns>
        string Build<TModel>(string templateContent, string templateCacheKey, TModel model);

        /// <summary>
        ///     Creates a string template bound to the model
        /// </summary>
        /// <typeparam name="TModel">Type of model class to use for binding</typeparam>
        /// <param name="viewTemplateFile">template file to use for view</param>
        /// <param name="templateCacheKey">key to cache template when compiling</param>
        /// <param name="model">model object to bind to view</param>
        /// <returns>string containing the model/view template evaluated</returns>
        string Build<TModel>(Uri viewTemplateFile, string templateCacheKey, TModel model);

        /// <summary>
        ///     Creates a string template bound to the model
        /// </summary>
        /// <typeparam name="TModel">Type of model class to use for binding</typeparam>
        /// <param name="viewTemplateFile">template file to use for view</param>
        /// <param name="model">model object to bind to view</param>
        /// <returns>string containing the model/view template evaluated</returns>
        string Build<TModel>(Uri viewTemplateFile, TModel model);
    }
}