// -----------------------------------------------------------------------
// <copyright file="IAction.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    using Abbotware.Core.Workflow.ExtensionPoints;
    using QuickGraph;

    /// <summary>
    /// Interface for an action
    /// </summary>
    public interface IAction : IWorkflowComponent, IEdge<IState>
    {
    }
}
