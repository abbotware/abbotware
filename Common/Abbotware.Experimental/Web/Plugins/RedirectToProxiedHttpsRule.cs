﻿// -----------------------------------------------------------------------
// <copyright file="RedirectToProxiedHttpsRule.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Web.Plugins
{
    using System.Linq;
    using System.Text;
    using Microsoft.AspNetCore.Rewrite;

    /// <summary>
    /// https://stackoverflow.com/questions/46701670/net-core-https-with-aws-load-balancer-and-elastic-beanstalk-doesnt-work
    /// </summary>
    public class RedirectToProxiedHttpsRule : IRule
    {
        /// <inheritdoc/>
        public virtual void ApplyRule(RewriteContext context)
        {
            Arguments.NotNull(context, nameof(context));

            var request = context.HttpContext.Request;

            // #1) Did this request start off as HTTP?
            string reqProtocol;

            if (request.Headers.TryGetValue("X-Forwarded-Proto", out var xforwardHeader))
            {
                reqProtocol = xforwardHeader.SingleOrDefault() ?? string.Empty;
            }
            else
            {
                reqProtocol = request.IsHttps ? "https" : "http";
            }

            // #2) If so, redirect to HTTPS equivalent
            if (reqProtocol != "https")
            {
                var newUrl = new StringBuilder()
                    .Append("https://").Append(request.Host)
                    .Append(request.PathBase).Append(request.Path)
                    .Append(request.QueryString);

                context.HttpContext.Response.Redirect(newUrl.ToString(), true);
            }
        }
    }
}
