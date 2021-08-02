// -----------------------------------------------------------------------
// <copyright file="ActionNotFoundException.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    /// <summary>
    /// Action not found configuration exception
    /// </summary>
    public class ActionNotFoundException : ConfigurationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionNotFoundException"/> class.
        /// </summary>
        /// <param name="action">action</param>
        public ActionNotFoundException(IAction action)
            : base($"Action:{action} {action?.Source}->{action?.Target} does not exist in state machine")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionNotFoundException"/> class.
        /// </summary>
        /// <param name="state">state</param>
        /// <param name="actionName">action name</param>
        public ActionNotFoundException(IState state, string actionName)
        : base($"State:{state?.Name} - Action:{actionName} does not exist in state machine")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionNotFoundException"/> class.
        /// </summary>
        /// <param name="actionName">action name</param>
        /// <param name="s">source state</param>
        /// <param name="t">target state</param>
        public ActionNotFoundException(string actionName, IState s, IState t)
            : base($"Action:{actionName} {s?.Name}->{t?.Name} does not exist in state machine")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionNotFoundException"/> class.
        /// </summary>
        /// <param name="actionName">action name</param>
        /// <param name="sourceName">source state name</param>
        /// <param name="targetName">target state name</param>
        public ActionNotFoundException(string actionName, string sourceName, string targetName)
            : base($"Action:{actionName} {sourceName}->{targetName} does not exist in state machine")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionNotFoundException"/> class.
        /// </summary>
        /// <param name="id">action id</param>
        public ActionNotFoundException(long id)
            : base($"Action:{id} Does not exist in state machine")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionNotFoundException"/> class.
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="innerException">inner exception</param>
        public ActionNotFoundException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionNotFoundException"/> class.
        /// </summary>
        protected ActionNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionNotFoundException"/> class.
        /// </summary>
        /// <param name="message">message</param>
        protected ActionNotFoundException(string message)
            : base(message)
        {
        }
    }
}