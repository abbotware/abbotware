// -----------------------------------------------------------------------
// <copyright file="IStateMachine.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    using System.Collections.Generic;
    using Abbotware.Core.Workflow.ExtensionPoints;

    public interface IStateMachine : IWorkflowComponent
    {
        IAction GetAction(long id);

        IState GetState(long id);

        IState GetState(string stateName);

        IEnumerable<IState> GetStates();

        IEnumerable<IAction> GetActions(string stateName);

        IEnumerable<IAction> GetActions(IState state);

        IAction ApplyAction(string stateName, string actionName);

        IAction ApplyAction(IState state, string actionName);
    }
}
