// -----------------------------------------------------------------------
// <copyright file="RewriteOptionsExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Web.Extensions
{
    using Abbotware.Core.Web.Plugins;
    using Microsoft.AspNetCore.Rewrite;

    /// <summary>
    /// https://stackoverflow.com/questions/46701670/net-core-https-with-aws-load-balancer-and-elastic-beanstalk-doesnt-work
    /// </summary>
    public static class RewriteOptionsExtensions
    {
        /// <summary>
        /// Adds redirect to proxied https
        /// </summary>
        /// <param name="options">rewriteOptions class being extended</param>
        /// <returns>RewriteOptions</returns>
        public static RewriteOptions AddRedirectToProxiedHttps(this RewriteOptions options)
        {
            Arguments.NotNull(options, nameof(options));

            options.Rules.Add(new RedirectToProxiedHttpsRule());

            return options;
        }
    }
}
