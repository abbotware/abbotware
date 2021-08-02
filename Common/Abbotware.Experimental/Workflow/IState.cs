// -----------------------------------------------------------------------
// <copyright file="IState.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    using Abbotware.Core.Workflow.ExtensionPoints;

    /// <summary>
    /// Interface for a state
    /// </summary>
    public interface IState : IWorkflowComponent
    {
        /// <summary>
        /// Gets a flag indicating whether or not this is the start state
        /// </summary>
        bool IsStart
        {
            get;
        }
    }
}
