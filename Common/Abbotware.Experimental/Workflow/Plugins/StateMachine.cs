// -----------------------------------------------------------------------
// <copyright file="StateMachine.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Workflow.ExtensionPoints;
    using QuickGraph;

    /// <summary>
    /// State Machine
    /// </summary>
    public class StateMachine : BaseWorkflowComponent, IStateMachine, IStateMachineManager
    {
        private static readonly IEnumerable<IAction> NoActions = new List<IAction>();

        private readonly BidirectionalGraph<IState, IAction> graph = new();

        private int stateCounter;

        private int edgeCounter;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateMachine"/> class.
        /// </summary>
        /// <param name="name">state machine name</param>
        /// <param name="id">state machine id</param>
        public StateMachine(string name, long id)
            : base(name, id)
        {
        }

        /// <inheritdoc/>
        IAction IStateMachineManager.AddAction(string action, string source, string target)
        {
            Arguments.NotNull(action, nameof(action));

            var fsm = this as IStateMachineManager;

            var s = this.FindOrCreateWorkflowState(source);

            var t = this.FindOrCreateWorkflowState(target);

            return fsm.AddAction(action, s, t);
        }

        /// <inheritdoc/>
        public IAction AddAction(string actionName, IState source, IState target)
        {
            Arguments.NotNull(actionName, nameof(actionName));

            var fsm = this as IStateMachineManager;

            var newAction = new ActionEdge(actionName, Interlocked.Increment(ref this.edgeCounter), source, target);

            fsm.AddAction(newAction);

            return newAction;
        }

        /// <inheritdoc/>
        public void AddAction(IAction action)
        {
            Arguments.NotNull(action, nameof(action));

            this.VerifyActionDoesNotExist(action);

            this.graph.AddVerticesAndEdge(action);
        }

        /// <inheritdoc/>
        public IState AddState(string state)
        {
            this.VerifyStateDoesNotExist(state);

            return this.OnCreateWorkflowState(state);
        }

        /// <inheritdoc/>
        public void AddState(IState state)
        {
            Arguments.NotNull(state, nameof(state));

            this.VerifyStateDoesNotExist(state);

            this.graph.AddVertex(state);
        }

        /// <inheritdoc/>
        public IAction GetAction(long id)
        {
            var action = this.FindWorkflowAction(id);

            if (action == null)
            {
                throw new ActionNotFoundException(id);
            }

            return action;
        }

        /// <inheritdoc/>
        public IState GetState(long id)
        {
            var state = this.FindWorkflowState(id);

            if (state == null)
            {
                throw new StateNotFoundException(id);
            }

            return state;
        }

        /// <inheritdoc/>
        public IState GetState(string stateName)
        {
            var state = this.FindWorkflowState(stateName);

            if (state == null)
            {
                throw new StateNotFoundException(stateName);
            }

            return state;
        }

        /// <inheritdoc/>
        public IEnumerable<IState> GetStates()
        {
            return this.graph.Vertices.ToList();
        }

        /// <inheritdoc/>
        public IEnumerable<IAction> GetActions(string stateName)
        {
            var state = this.FindWorkflowState(stateName);

            if (state == null)
            {
                throw new StateNotFoundException(stateName);
            }

            return this.GetActions(state);
        }

        /// <inheritdoc/>
        public IEnumerable<IAction> GetActions(IState state)
        {
            Arguments.NotNull(state, nameof(state));

            if (!this.graph.TryGetOutEdges(state, out var edges))
            {
                return NoActions;
            }

            return edges;
        }

        /// <inheritdoc/>
        public IAction ApplyAction(string stateName, string actionName)
        {
            var s = this.GetState(stateName);

            return this.ApplyAction(s, actionName);
        }

        /// <inheritdoc/>
        public IAction ApplyAction(IState state, string actionName)
        {
            state = Arguments.EnsureNotNull(state, nameof(state));
            actionName = Arguments.EnsureNotNull(actionName, nameof(actionName));

            var a = this.GetActions(state);

            var action = a.Where(x => string.Equals(x.Name, actionName, StringComparison.OrdinalIgnoreCase));

            if (!action.Any())
            {
                ////var journal = new WorkflowJournal(this.Name, this.Logger);
                ////var j = journal as IEditableWorkflowJournal;

                ////var message = string.Format("Cannot Apply Action:{0} to object:{1} when it in state:{2}", actionName, typeof(TObject).Name, currentState);
                ////j.NonExemptibleErrorFormat(current.GetType().Name, this.WorkflowObjectInformation.GetGroupKey(current), current, "Action Not Allowed", "Action Not Allowed", message);
                ////return journal;

                throw new ActionNotFoundException(state, actionName);
            }

            if (action.Count() > 1)
            {
                throw new InvalidOperationException($"Too many actions found for:{actionName}");
            }

            return action.Single();
        }

        /// <summary>
        /// Callback hook for custom logic when creating a workflow state
        /// </summary>
        /// <param name="stateName">state name</param>
        /// <returns>state</returns>
        protected virtual State OnCreateWorkflowState(string stateName)
        {
            State newState;

            if (stateName.IsBlank())
            {
                newState = State.NullState;
            }
            else
            {
                newState = new State(stateName, Interlocked.Increment(ref this.stateCounter), false);
            }

            this.graph.AddVertex(newState);

            return newState;
        }

        /// <summary>
        /// Helper method to verify action does not exist
        /// </summary>
        /// <param name="action">action</param>
        protected void VerifyActionDoesNotExist(IAction action)
        {
            action = Arguments.EnsureNotNull(action, nameof(action));

            if (this.FindWorkflowAction(action.Id) != null)
            {
                throw new DuplicateActionException(action);
            }

            this.graph.TryGetEdges(action.Source, action.Target, out var edges);

            if (edges == null)
            {
                return;
            }

            if (edges.Any(x => x.Name.Equals(action.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new DuplicateActionException(action.Name, action.Source, action.Target);
            }
        }

        /// <summary>
        /// Helper method to verify state does not exist
        /// </summary>
        /// <param name="state">state</param>
        protected void VerifyStateDoesNotExist(IState state)
        {
            state = Arguments.EnsureNotNull(state, nameof(state));

            if (this.FindWorkflowState(state.Name) != null)
            {
                throw new DuplicateStateException(state);
            }

            if (this.FindWorkflowState(state.Id) != null)
            {
                throw new DuplicateStateException(state);
            }
        }

        /// <summary>
        /// Helper method to verify state does not exists
        /// </summary>
        /// <param name="stateName">state name</param>
        protected void VerifyStateDoesNotExist(string stateName)
        {
            var v = this.FindWorkflowState(stateName);

            if (v != null)
            {
                throw new DuplicateStateException(stateName);
            }
        }

        /// <summary>
        /// Config Change Helper - Verify state does not exist
        /// </summary>
        /// <param name="stateName">state name</param>
        /// <returns>state</returns>
        protected State VerifyStateExistsDuringConfigChange(string stateName)
        {
            var v = this.FindWorkflowState(stateName);

            if (v == null)
            {
                throw new StateNotFoundException(stateName);
            }

            return v;
        }

        /// <summary>
        /// Config Change Helper - Verify state exists
        /// </summary>
        /// <param name="state">state</param>
        protected void VerifyContainsStateDuringConfigChange(State state)
        {
            if (!this.graph.ContainsVertex(state))
            {
                throw new StateNotFoundException(state);
            }
        }

        /// <summary>
        /// Config Change Helper - Verify action exists
        /// </summary>
        /// <param name="actionName">action name</param>
        /// <param name="sourceStateName">source state name</param>
        /// <param name="targetStateName">target state name</param>
        /// <returns>action</returns>
        protected ActionEdge VerifyActionExistsDuringConfigChange(string actionName, string sourceStateName, string targetStateName)
        {
            var source = this.VerifyStateExistsDuringConfigChange(sourceStateName);
            var target = this.VerifyStateExistsDuringConfigChange(targetStateName);

            return this.VerifyActionExistsDuringConfigChange(actionName, source, target);
        }

        /// <summary>
        /// Config Change Helper - Verify action exists
        /// </summary>
        /// <param name="action">action</param>
        protected void VerifyActionExistsDuringConfigChange(ActionEdge action)
        {
            if (!this.graph.ContainsEdge(action))
            {
                throw new ActionNotFoundException(action);
            }
        }

        /// <summary>
        /// Config Change Helper - Verify action exists
        /// </summary>
        /// <param name="actionName">action name</param>
        /// <param name="s">source state</param>
        /// <param name="t">target state</param>
        /// <returns>action</returns>
        protected ActionEdge VerifyActionExistsDuringConfigChange(string actionName, State s, State t)
        {
            this.graph.TryGetEdges(s, t, out var edges);

            if (edges == null)
            {
                throw new ActionNotFoundException(actionName, s, t);
            }

            var edge = edges.SingleOrDefault(x => x.Name == actionName);

            if (edge == null)
            {
                throw new ActionNotFoundException(actionName, s, t);
            }

            return (ActionEdge)edge;
        }

        /// <summary>
        /// Finds a state by name
        /// </summary>
        /// <param name="stateName">state name</param>
        /// <returns>state if found</returns>
        protected State? FindWorkflowState(string stateName)
        {
            if (stateName == null)
            {
                stateName = string.Empty;
            }

            return (State)this.graph.Vertices.SingleOrDefault(v => string.Equals(v.Name, stateName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Finds or creates a state by name
        /// </summary>
        /// <param name="stateName">state name</param>
        /// <returns>state if found or created</returns>
        protected State FindOrCreateWorkflowState(string stateName)
        {
            var state = this.FindWorkflowState(stateName);

            if (state == null)
            {
                state = this.OnCreateWorkflowState(stateName);
            }

            return state;
        }

        /// <summary>
        /// Finds or creates a state by id
        /// </summary>
        /// <param name="stateId">state id</param>
        /// <returns>state if found</returns>
        protected State? FindWorkflowState(long stateId)
        {
            return this.graph.Vertices.SingleOrDefault(v => v.Id == stateId) as State;
        }

        /// <summary>
        /// Finds an action by id
        /// </summary>
        /// <param name="actionId">action id</param>
        /// <returns>action if found</returns>
        protected ActionEdge? FindWorkflowAction(long actionId)
        {
            return this.graph.Edges.SingleOrDefault(e => e.Id == actionId) as ActionEdge;
        }
    }
}
