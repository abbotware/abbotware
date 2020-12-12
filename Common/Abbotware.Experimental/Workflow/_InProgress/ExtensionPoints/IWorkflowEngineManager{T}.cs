// -----------------------------------------------------------------------
// <copyright file="IWorkflowEngineManager{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow.ExtensionPoints
{
    public interface IWorkflowEngineManager<T> : IStateMachineManager, IWorkflowPartsInformation
       where T : class
    {
        void AddRuleToStateOnEnter(IState state, BaseWorkflowRule part);

        void AddRuleToStateOnEnter(string state, BaseWorkflowRule part);

        void AddRuleToStateOnExit(IState state, BaseWorkflowRule part);

        void AddRuleToStateOnExit(string state, BaseWorkflowRule part);

        void AddRuleToAction(string action, string source, string target, BaseWorkflowRule part);

        void AddRuleToAction(IAction action, BaseWorkflowRule part);

        //// --- following were removed from base state machine

        void SetInitialState(IState state);

        ////IAction AddAction(string action, long id, IState source, IState target, bool isAutomatic);

        ////IAction AddAction(string action, long id, IState source, IState target, bool isAutomatic, long? linkedActionId, string linkedActionName);

        void AddPartToStateOnEnter(string state, BaseWorkflowPart part);

        void AddPartToStateOnEnter(IState state, BaseWorkflowPart part);

        void AddPartToStateOnExit(string state, BaseWorkflowPart part);

        void AddPartToStateOnExit(IState state, BaseWorkflowPart part);

        void AddPartToAction(string action, string source, string target, BaseWorkflowPart part);

        void AddPartToAction(IAction action, BaseWorkflowPart part);

        void AddPermissionToAction(IAction action, string permission);
    }
}
