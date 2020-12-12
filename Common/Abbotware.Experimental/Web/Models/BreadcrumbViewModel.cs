// -----------------------------------------------------------------------
// <copyright file="BreadcrumbViewModel.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Web.Models
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// view model for breadcrumbs
    /// </summary>
    public class BreadcrumbViewModel
    {
        /// <summary>
        /// Gets or sets the text label for the breadcrumb
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is the last breadcrumb
        /// </summary>
        public bool IsLast { get; set; }

        /// <summary>
        /// Gets or sets Uri fragment for breadcrumb link
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "reviewed")]
        public string UriFragment { get; set; }
    }
}