// -----------------------------------------------------------------------
// <copyright file="PageViewModel.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Web.Models
{
    /// <summary>
    /// basic viewmodel for a page
    /// </summary>
    public class PageViewModel
    {
        /// <summary>
        /// Gets or sets the page title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the page message
        /// </summary>
        public string Message { get; set; }
    }
}