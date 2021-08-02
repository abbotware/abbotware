// -----------------------------------------------------------------------
// <copyright file="IStateMachine{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for a StateMachine based on extern object T
    /// </summary>
    /// <typeparam name="T">external object type</typeparam>
    public interface IStateMachine<T> : IStateMachine
    {
        /// <summary>
        /// apply an action for the provided workflow object
        /// </summary>
        /// <param name="current">current workflow object</param>
        /// <param name="actionName">action name</param>
        /// <returns>journal of events</returns>
        IJournal ApplyAction(T current, string actionName);

        /// <summary>
        /// Determine actions for the provided workflow object
        /// </summary>
        /// <param name="current">current workflow object</param>
        /// <returns>list of actions applicable to the current object</returns>
        IEnumerable<IAction> DetermineActions(T current);

        /// <summary>
        /// Determine the state of the provided workflow object
        /// </summary>
        /// <param name="current">current workflow object</param>
        /// <returns>the state</returns>
        IState DetermineState(T current);
    }
}
