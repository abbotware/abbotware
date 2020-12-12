// -----------------------------------------------------------------------
// <copyright file="StateMachine{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Abbotware.Core.Workflow.Configuration.Models;

    public class StateMachine<T> : StateMachine, IStateMachine<T>
    {
        public StateMachine(Func<T, string> retrieve, Action<T, string> store,  string name, long id)
            : this(new StateMachineConfiguration<T>(retrieve, store), name, id)
        {
        }

        public StateMachine(StateMachineConfiguration<T> config, string name, long id)
            : base(name, id)
        {
            this.Config = config;
        }

        protected StateMachineConfiguration<T> Config { get; }

        public IJournal ApplyAction(T item, string actionName)
        {
            var s = this.DetermineState(item);

            var a = this.ApplyAction(s, actionName);

            if (a.Source != a.Target)
            {
                this.Config.StoreStateFunctor(item, a.Target.Name);
            }

            return null;
        }

        public IEnumerable<IAction> DetermineActions(T item)
        {
            Arguments.NotNull(item, nameof(item));

            var v = this.DetermineState(item);

            Debug.Assert(v != null, "object state must be valid");

            return this.GetActions(v);
        }

        public IState DetermineState(T item)
        {
            Arguments.NotNull(item, nameof(item));

            var stateName = this.Config.RetrieveStateFunctor(item);

            var state = this.FindWorkflowState(stateName);

            if (state == null)
            {
                throw new IndeterminateStateException(item, stateName, this.Name);
            }

            return state;
        }
    }
}
