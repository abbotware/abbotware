// -----------------------------------------------------------------------
// <copyright file="Breadcrumb.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Web.ExtensionPoints
{
    using Abbotware.Core.Web.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    /// <summary>
    /// Bread crumb case class
    /// </summary>
    public class Breadcrumb
    {
        /// <summary>
        /// Gets or sets the text of the bread crumb
        /// </summary>
        public virtual string Text { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the controller for the the bread crumb
        /// </summary>
        public string Controller { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the action for the bread crumb
        /// </summary>
        public string Action { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the uri fragment of the bread crumb
        /// </summary>
        public string Fragment { get; set; } = "~";

        /// <summary>
        /// Gets the Default key for node
        /// </summary>
        protected string DefaultKey => $"{this.Controller}.{this.Action}";

        /// <summary>
        /// Creates a breadcrumb viewmodel based on the http context
        /// </summary>
        /// <param name="context">current http context </param>
        /// <returns>view model</returns>
        public BreadcrumbViewModel CreateViewModel(HttpContext context)
        {
            var vm = new BreadcrumbViewModel
            {
                Text = this.Text,
                UriFragment = $"/{this.Controller}/{this.Action}/",
            };

            this.OnConfigureViewModel(vm, context);

            return vm;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var result = $" Text: '{this.Text}'  Controller: '{this.Controller}'  Action: '{this.Action}' ";
            return result;
        }

        /// <summary>
        /// Hook to customize view model
        /// </summary>
        /// <param name="viewModel">view model</param>
        /// <param name="context">http context</param>
        protected virtual void OnConfigureViewModel(BreadcrumbViewModel viewModel, HttpContext context)
        {
        }

        /// <summary>
        /// Gets the key based on the corrent route data
        /// </summary>
        /// <param name="routeData">route data</param>
        /// <returns>key</returns>
        protected virtual string RouteKey(RouteData routeData)
        {
            Arguments.NotNull(routeData, nameof(routeData));

            var actionName = routeData.Values["action"].ToString();
            var controllerName = routeData.Values["controller"].ToString();

            return $"{controllerName}.{actionName}";
        }
    }
}