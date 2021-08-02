// -----------------------------------------------------------------------
// <copyright file="IStateMachineManager.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow.ExtensionPoints
{
    /// <summary>
    /// Interface for managing a State Machine
    /// </summary>
    public interface IStateMachineManager : IStateMachine
    {
        /// <summary>
        /// Add a state
        /// </summary>
        /// <param name="state">state</param>
        void AddState(IState state);

        /// <summary>
        /// Add a state
        /// </summary>
        /// <param name="stateName">state name</param>
        /// <returns>created state</returns>
        IState AddState(string stateName);

        /// <summary>
        /// Add an action
        /// </summary>
        /// <param name="actionName">action name</param>
        /// <param name="source">source state</param>
        /// <param name="target">target state</param>
        /// <returns>created action</returns>
        IAction AddAction(string actionName, IState source, IState target);

        /// <summary>
        /// Add an action
        /// </summary>
        /// <param name="actionName">action name</param>
        /// <param name="sourceStateName">source state name</param>
        /// <param name="targetStateName">target state target</param>
        /// <returns>created action</returns>
        IAction AddAction(string actionName, string sourceStateName, string targetStateName);

        /// <summary>
        /// Add an action
        /// </summary>
        /// <param name="action">action</param>
        void AddAction(IAction action);
    }
}