// -----------------------------------------------------------------------
// <copyright file="ListItemViewModel.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Web.Models
{
    /// <summary>
    ///     view model for data list item
    /// </summary>
    public class ListItemViewModel
    {
        /// <summary>
        ///     Gets or sets the Id value of the list item
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Gets or sets the name of the list item
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the description of the list item
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}