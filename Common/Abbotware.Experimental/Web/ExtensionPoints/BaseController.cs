// -----------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Web.ExtensionPoints
{
    using Abbotware.Core.Logging;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// base class for creating MVC controller
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="logger">injected logger</param>
        protected BaseController(ILogger logger)
        {
            this.Logger = logger;
        }

        /// <summary>
        /// Gets the logger
        /// </summary>
        public ILogger Logger { get; }
    }
}