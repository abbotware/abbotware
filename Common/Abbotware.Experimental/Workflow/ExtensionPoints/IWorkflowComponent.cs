// -----------------------------------------------------------------------
// <copyright file="IWorkflowComponent.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow.ExtensionPoints
{
    /// <summary>
    /// Interface for a workflow component
    /// </summary>
    public interface IWorkflowComponent
    {
        /// <summary>
        /// Gets the Id of the component
        /// </summary>
        long Id { get; }

        /// <summary>
        /// Gets the name of the component
        /// </summary>
        string Name { get; }
    }
}
