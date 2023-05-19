// -----------------------------------------------------------------------
// <copyright file="StateMachine{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Abbotware.Core.Workflow.Configuration.Models;

    /// <summary>
    /// State Machine based on extern object T
    /// </summary>
    /// <typeparam name="T">external object type</typeparam>
    public class StateMachine<T> : StateMachine, IStateMachine<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateMachine{T}"/> class.
        /// </summary>
        /// <param name="retrieve">retrieve state delegate</param>
        /// <param name="store">store state delegate</param>
        /// <param name="name">state machine name</param>
        /// <param name="id">state machine id</param>
        public StateMachine(Func<T, string> retrieve, Action<T, string> store, string name, long id)
            : this(new StateMachineConfiguration<T>(retrieve, store), name, id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateMachine{T}"/> class.
        /// </summary>
        /// <param name="config">configuration</param>
        /// <param name="name">state machine name</param>
        /// <param name="id">state machine id</param>
        public StateMachine(StateMachineConfiguration<T> config, string name, long id)
            : base(name, id)
        {
            this.Config = config;
        }

        /// <summary>
        /// gets the configuration
        /// </summary>
        protected StateMachineConfiguration<T> Config { get; }

        /// <inheritdoc/>
        public IJournal ApplyAction(T current, string actionName)
        {
            var s = this.DetermineState(current);

            var a = this.ApplyAction(s, actionName);

            if (a.Source != a.Target)
            {
                this.Config.StoreStateFunctor(current, a.Target.Name);
            }

            return new Journal();
        }

        /// <inheritdoc/>
        public IEnumerable<IAction> DetermineActions(T current)
        {
            Arguments.NotNull(current, nameof(current));

            var v = this.DetermineState(current);

            Debug.Assert(v != null, "object state must be valid");

            return this.GetActions(v);
        }

        /// <inheritdoc/>
        public IState DetermineState(T current)
        {
            Arguments.NotNull(current, nameof(current));

            var stateName = this.Config.RetrieveStateFunctor(current);

            var state = this.FindWorkflowState(stateName);

            if (state == null)
            {
                throw new IndeterminateStateException(current, stateName, this.Name);
            }

            return state;
        }
    }
}
