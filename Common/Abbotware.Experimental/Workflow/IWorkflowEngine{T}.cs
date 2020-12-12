// -----------------------------------------------------------------------
// <copyright file="IWorkflowEngine{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    using System.Collections.Generic;

    public interface IWorkflowEngine<T> : IStateMachine<T>
    {
        IEnumerable<string> ActionRequirements(T current, string actionName);

        IEnumerable<string> ActionRequirements(string stateName, string actionName);

        IEnumerable<string> ActionRequirements(IState state, string actionName);

        IEnumerable<string> ActionRequirements(IAction action);

        IEnumerable<string> StateRequirements(string stateName);

        IEnumerable<string> StateRequirements(IState state);
    }
}
