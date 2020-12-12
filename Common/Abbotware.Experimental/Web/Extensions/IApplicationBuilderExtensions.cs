// -----------------------------------------------------------------------
// <copyright file="IApplicationBuilderExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Web.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Rewrite;

    /// <summary>
    /// ASP Net Core Application builder extensions
    /// </summary>
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds HTTPS Proxy Redirect (for AWS EBS)
        /// </summary>
        /// <param name="app">app builder</param>
        public static void AddProxyHttpsRedirect(this IApplicationBuilder app)
        {
            var options = new RewriteOptions()
                  .AddRedirectToProxiedHttps()
                  .AddRedirect("(.*)/$", "$1");  // remove trailing slash

            app.UseRewriter(options);
        }
    }
}
