//-----------------------------------------------------------------------
// <copyright file="HtmlExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.Core.Web.Helpers
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Html helper extension methods
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// creates a html action link
        /// </summary>
        /// <param name="htmlHelper">html helper</param>
        /// <param name="linkText">link text</param>
        /// <param name="action">name of action</param>
        /// <param name="controller">name of controller</param>
        /// <param name="htmlAttributes">html attributes</param>
        /// <returns>html string</returns>
        public static IHtmlString RawActionLink(this HtmlHelper htmlHelper, string linkText, string action, string controller, object htmlAttributes)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            var anchor = new TagBuilder("a")
            {
                InnerHtml = linkText
            };

            anchor.Attributes["href"] = urlHelper.Action(action, controller);
            anchor.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(anchor.ToString());
        }
    }
}