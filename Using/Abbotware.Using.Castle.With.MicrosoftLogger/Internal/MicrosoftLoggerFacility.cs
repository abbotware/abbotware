// -----------------------------------------------------------------------
// <copyright file="MicrosoftLoggerFacility.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Using.Castle.Internal
{
    using global::Castle.MicroKernel.Facilities;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Abbotware Castle Facility for Logging
    /// </summary>
    public class MicrosoftLoggerFacility : AbstractFacility
    {
        /// <inheritdoc />
        protected override void Init()
        {
            var factory = this.Kernel.Resolve<ILoggerFactory>();

            this.Kernel.Resolver.AddSubResolver(new MicrosoftLoggerResolver(factory));
        }
    }
}