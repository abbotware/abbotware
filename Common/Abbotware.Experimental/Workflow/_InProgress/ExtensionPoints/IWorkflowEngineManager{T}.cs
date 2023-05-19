// -----------------------------------------------------------------------
// <copyright file="IWorkflowEngineManager{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow.ExtensionPoints
{
    /// <summary>
    /// Interface for editing a workflow
    /// </summary>
    /// <typeparam name="T">external object type</typeparam>
    public interface IWorkflowEngineManager<T> : IStateMachineManager, IWorkflowPartsInformation
       where T : class
    {
        /// <summary>
        /// Add a rule to OnEnter state
        /// </summary>
        /// <param name="state">state</param>
        /// <param name="rule">rule</param>
        void AddRuleToStateOnEnter(IState state, BaseWorkflowRule rule);

        /// <summary>
        /// Add a rule to OnEnter state
        /// </summary>
        /// <param name="stateName">state name</param>
        /// <param name="rule">rule</param>
        void AddRuleToStateOnEnter(string stateName, BaseWorkflowRule rule);

        /// <summary>
        /// Add a rule to OnExit state
        /// </summary>
        /// <param name="state">state</param>
        /// <param name="rule">rule</param>
        void AddRuleToStateOnExit(IState state, BaseWorkflowRule rule);

        /// <summary>
        /// Add a rule to OnExit state
        /// </summary>
        /// <param name="stateName">state name</param>
        /// <param name="rule">rule</param>
        void AddRuleToStateOnExit(string stateName, BaseWorkflowRule rule);

        /// <summary>
        /// Add a rule to OnAction transition
        /// </summary>
        /// <param name="actionName">action name</param>
        /// <param name="sourceStateName">source state name</param>
        /// <param name="targetStateName">target state name</param>
        /// <param name="rule">rule</param>
        void AddRuleToAction(string actionName, string sourceStateName, string targetStateName, BaseWorkflowRule rule);

        /// <summary>
        /// Add a rule to OnAction transition
        /// </summary>
        /// <param name="action">action</param>
        /// <param name="rule">part</param>
        void AddRuleToAction(IAction action, BaseWorkflowRule rule);

        //// --- following were removed from base state machine

        /// <summary>
        /// Set the intial state
        /// </summary>
        /// <param name="state">state</param>
        void SetInitialState(IState state);

        ////IAction AddAction(string action, long id, IState source, IState target, bool isAutomatic);

        ////IAction AddAction(string action, long id, IState source, IState target, bool isAutomatic, long? linkedActionId, string linkedActionName);

        /// <summary>
        /// Add a part to OnEnter state
        /// </summary>
        /// <param name="stateName">state name</param>
        /// <param name="part">part</param>
        void AddPartToStateOnEnter(string stateName, BaseWorkflowPart part);

        /// <summary>
        /// Add a part to OnEnter state
        /// </summary>
        /// <param name="state">state</param>
        /// <param name="part">part</param>
        void AddPartToStateOnEnter(IState state, BaseWorkflowPart part);

        /// <summary>
        /// Add a part to OnExit state
        /// </summary>
        /// <param name="stateName">state name</param>
        /// <param name="part">part</param>
        void AddPartToStateOnExit(string stateName, BaseWorkflowPart part);

        /// <summary>
        /// Add a part to OnExit state
        /// </summary>
        /// <param name="state">state</param>
        /// <param name="part">part</param>
        void AddPartToStateOnExit(IState state, BaseWorkflowPart part);

        /// <summary>
        /// Add a part to OnAction transition
        /// </summary>
        /// <param name="actionName">action name</param>
        /// <param name="sourceStateName">source state name</param>
        /// <param name="targetStateName">target state name</param>
        /// <param name="part">part</param>
        void AddPartToAction(string actionName, string sourceStateName, string targetStateName, BaseWorkflowPart part);

        /// <summary>
        /// Add a part to OnAction transition
        /// </summary>
        /// <param name="action">action</param>
        /// <param name="part">part</param>
        void AddPartToAction(IAction action, BaseWorkflowPart part);

        /// <summary>
        /// Add Permission to an action
        /// </summary>
        /// <param name="action">action</param>
        /// <param name="permissionName">permissionName</param>
        void AddPermissionToAction(IAction action, string permissionName);
    }
}
