// -----------------------------------------------------------------------
// <copyright file="IBreadcrumbManager.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Web.ExtensionPoints
{
    using System.Collections.Generic;
    using Abbotware.Core.Web.Models;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Interface for breadcrumb manager
    /// </summary>
    public interface IBreadcrumbManager
    {
        /// <summary>
        /// Creates breadcrumb view models for the current http context based on route info
        /// </summary>
        /// <param name="context">current http context</param>
        /// <returns>view models</returns>
        IEnumerable<BreadcrumbViewModel> Build(HttpContext context);
    }
}