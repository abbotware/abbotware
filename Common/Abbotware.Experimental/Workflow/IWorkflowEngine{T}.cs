// -----------------------------------------------------------------------
// <copyright file="IWorkflowEngine{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for a Workflow Engine which is an extended State Machine
    /// </summary>
    /// <typeparam name="T">external object type</typeparam>
    public interface IWorkflowEngine<T> : IStateMachine<T>
    {
        /// <summary>
        /// Gets the action requirements
        /// </summary>
        /// <param name="current">external object</param>
        /// <param name="actionName">action name</param>
        /// <returns>list of requirements</returns>
        IEnumerable<string> ActionRequirements(T current, string actionName);

        /// <summary>
        /// Gets the action requirements
        /// </summary>
        /// <param name="stateName">state name</param>
        /// <param name="actionName">action name</param>
        /// <returns>list of requirements</returns>
        IEnumerable<string> ActionRequirements(string stateName, string actionName);

        /// <summary>
        /// Gets the action requirements
        /// </summary>
        /// <param name="state">state</param>
        /// <param name="actionName">action name</param>
        /// <returns>list of requirements</returns>
        IEnumerable<string> ActionRequirements(IState state, string actionName);

        /// <summary>
        /// Gets the action requirements
        /// </summary>
        /// <param name="action">action</param>
        /// <returns>list of requirements</returns>
        IEnumerable<string> ActionRequirements(IAction action);

        /// <summary>
        /// Gets the state requirements
        /// </summary>
        /// <param name="stateName">state name</param>
        /// <returns>list of requirements</returns>
        IEnumerable<string> StateRequirements(string stateName);

        /// <summary>
        /// Gets the state requirements
        /// </summary>
        /// <param name="state">state</param>
        /// <returns>list of requirements</returns>
        IEnumerable<string> StateRequirements(IState state);
    }
}
