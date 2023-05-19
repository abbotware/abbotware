// -----------------------------------------------------------------------
// <copyright file="IJournal.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    /// <summary>
    /// interface for a workflow journal
    /// </summary>
    public interface IJournal
    {
        /// <summary>
        /// Adds to the journal
        /// </summary>
        void Add();
    }

    /// <summary>
    /// Workflow journal
    /// </summary>
    public class Journal : IJournal
    {
        /// <inheritdoc/>
        public void Add()
        {
        }
    }
}
