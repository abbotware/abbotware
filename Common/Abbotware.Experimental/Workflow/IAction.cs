// -----------------------------------------------------------------------
// <copyright file="IAction.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    using Abbotware.Core.Workflow.ExtensionPoints;
    using QuickGraph;

    public interface IAction : IWorkflowComponent, IEdge<IState>
    {
    }
}
