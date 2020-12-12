// -----------------------------------------------------------------------
// <copyright file="IWorkflowComponent.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow.ExtensionPoints
{
    public interface IWorkflowComponent
    {
        long Id { get; }

        string Name { get; }
    }
}
