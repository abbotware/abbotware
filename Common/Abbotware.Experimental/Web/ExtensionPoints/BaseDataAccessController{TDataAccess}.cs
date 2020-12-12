// -----------------------------------------------------------------------
// <copyright file="BaseDataAccessController{TDataAccess}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Web.ExtensionPoints
{
    using System;
    using Abbotware.Core.Logging;

    /// <summary>
    /// Base class for a controller with data access
    /// </summary>
    /// <typeparam name="TDataAccess">data access class type</typeparam>
    public abstract class BaseDataAccessController<TDataAccess> : BaseController
        where TDataAccess : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDataAccessController{TDataAccess}"/> class.
        /// </summary>
        /// <param name="dataAccess">data access</param>
        /// <param name="logger">injected logger</param>
        protected BaseDataAccessController(TDataAccess dataAccess, ILogger logger)
            : base(logger)
        {
            this.DataAccess = dataAccess;
        }

        /// <summary>
        /// Gets the data access object
        /// </summary>
        protected TDataAccess DataAccess { get; }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DataAccess?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}