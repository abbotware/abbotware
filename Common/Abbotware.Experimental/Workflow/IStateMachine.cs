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

    /// <summary>
    /// Interface for a state machine
    /// </summary>
    public interface IStateMachine : IWorkflowComponent
    {
        /// <summary>
        /// Gets an action by id
        /// </summary>
        /// <param name="id">action id</param>
        /// <returns>the action</returns>
        IAction GetAction(long id);

        /// <summary>
        /// Gets a state by id
        /// </summary>
        /// <param name="id">state id</param>
        /// <returns>the state</returns>
        IState GetState(long id);

        /// <summary>
        /// Gets a state by name
        /// </summary>
        /// <param name="stateName">state name</param>
        /// <returns>the state</returns>
        IState GetState(string stateName);

        /// <summary>
        /// Gets all States in the state machine
        /// </summary>
        /// <returns>list of all state</returns>
        IEnumerable<IState> GetStates();

        /// <summary>
        /// Gets all actions for the specified state
        /// </summary>
        /// <param name="stateName">state name</param>
        /// <returns>list of all actions for the specified state</returns>
        IEnumerable<IAction> GetActions(string stateName);

        /// <summary>
        /// Gets all actions for the specified state
        /// </summary>
        /// <param name="state">state</param>
        /// <returns>list of all actions for the specified state</returns>
        IEnumerable<IAction> GetActions(IState state);

        /// <summary>
        /// apply an action for the specified state
        /// </summary>
        /// <param name="stateName">state name</param>
        /// <param name="actionName">action name to apply</param>
        /// <returns>the applied action</returns>
        IAction ApplyAction(string stateName, string actionName);

        /// <summary>
        /// apply an action for the specified state
        /// </summary>
        /// <param name="state">state</param>
        /// <param name="actionName">action name to apply</param>
        /// <returns>the applied action</returns>
        IAction ApplyAction(IState state, string actionName);
    }
}
