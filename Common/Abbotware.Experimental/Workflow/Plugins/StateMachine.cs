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
    using Abbotware.Core.Workflow.ExtensionPoints;
    using QuickGraph;

    public class StateMachine : BaseWorkflowComponent, IStateMachine, IStateMachineManager
    {
        private static readonly IEnumerable<IAction> NoActions = new List<IAction>();

        private readonly BidirectionalGraph<IState, IAction> graph = new BidirectionalGraph<IState, IAction>();

        private int stateCounter;

        private int edgeCounter;

        public StateMachine(string name, long id)
            : base(name, id)
        {
        }

        IAction IStateMachineManager.AddAction(string action, string source, string target)
        {
            Arguments.NotNull(action, nameof(action));

            var fsm = this as IStateMachineManager;

            var s = this.FindOrCreateWorkflowState(source);

            var t = this.FindOrCreateWorkflowState(target);

            return fsm.AddAction(action, s, t);
        }

        public IAction AddAction(string action, IState source, IState target)
        {
            Arguments.NotNull(action, nameof(action));

            var fsm = this as IStateMachineManager;

            var newAction = new ActionEdge(action, Interlocked.Increment(ref this.edgeCounter), source, target);

            fsm.AddAction(newAction);

            return newAction;
        }

        public void AddAction(IAction action)
        {
            Arguments.NotNull(action, nameof(action));

            this.VerifyActionDoesNotExist(action);

            this.graph.AddVerticesAndEdge(action);
        }

        public IState AddState(string state)
        {
            this.VerifyStateDoesNotExist(state);

            return this.OnCreateWorkflowState(state);
        }

        public void AddState(IState state)
        {
            Arguments.NotNull(state, nameof(state));

            this.VerifyStateDoesNotExist(state);

            this.graph.AddVertex(state);
        }

        public IAction GetAction(long id)
        {
            var action = this.FindWorkflowAction(id);

            if (action == null)
            {
                throw new ActionNotFoundException(id);
            }

            return action;
        }

        public IState GetState(long id)
        {
            var state = this.FindWorkflowState(id);

            if (state == null)
            {
                throw new StateNotFoundException(id);
            }

            return state;
        }

        public IState GetState(string stateName)
        {
            var state = this.FindWorkflowState(stateName);

            if (state == null)
            {
                throw new StateNotFoundException(stateName);
            }

            return state;
        }

        public IEnumerable<IState> GetStates()
        {
            return this.graph.Vertices.ToList();
        }

        public IEnumerable<IAction> GetActions(string stateName)
        {
            var state = this.FindWorkflowState(stateName);

            if (state == null)
            {
                throw new StateNotFoundException(stateName);
            }

            return this.GetActions(state);
        }

        public IEnumerable<IAction> GetActions(IState state)
        {
            Arguments.NotNull(state, nameof(state));

            if (!this.graph.TryGetOutEdges(state, out var edges))
            {
                return NoActions;
            }

            return edges;
        }

        public IAction ApplyAction(string stateName, string actionName)
        {
            var s = this.GetState(stateName);

            return this.ApplyAction(s, actionName);
        }

        public IAction ApplyAction(IState state, string actionName)
        {
            Arguments.NotNull(state, nameof(state));
            Arguments.NotNull(actionName, nameof(actionName));

            var a = this.GetActions(state);

            var action = a.Where(x => string.Equals(x.Name, actionName, StringComparison.InvariantCultureIgnoreCase));

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

        protected void VerifyActionDoesNotExist(IAction action)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            if (this.FindWorkflowAction(action.Id) != null)
#pragma warning restore CA1062 // Validate arguments of public methods
            {
                throw new DuplicateActionException(action);
            }

            this.graph.TryGetEdges(action.Source, action.Target, out var edges);

            if (edges == null)
            {
                return;
            }

            if (edges.Any(x => x.Name.Equals(action.Name, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new DuplicateActionException(action.Name, action.Source, action.Target);
            }
        }

        protected void VerifyStateDoesNotExist(IState v)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            if (this.FindWorkflowState(v.Name) != null)
#pragma warning restore CA1062 // Validate arguments of public methods
            {
                throw new DuplicateStateException(v);
            }

            if (this.FindWorkflowState(v.Id) != null)
            {
                throw new DuplicateStateException(v);
            }
        }

        protected State VerifyStateExistsDuringConfigChange(string name)
        {
            var v = this.FindWorkflowState(name);

            if (v == null)
            {
                throw new StateNotFoundException(name);
            }

            return v;
        }

        protected void VerifyContainsStateDuringConfigChange(State state)
        {
            if (!this.graph.ContainsVertex(state))
            {
                throw new StateNotFoundException(state);
            }
        }

        protected void VerifyStateDoesNotExist(string name)
        {
            var v = this.FindWorkflowState(name);

            if (v != null)
            {
                throw new DuplicateStateException(name);
            }
        }

        protected ActionEdge VerifyActionExistsDuringConfigChange(string action, string s, string t)
        {
            var source = this.VerifyStateExistsDuringConfigChange(s);
            var target = this.VerifyStateExistsDuringConfigChange(t);

            return this.VerifyActionExistsDuringConfigChange(action, source, target);
        }

        protected void VerifyActionExistsDuringConfigChange(ActionEdge action)
        {
            if (!this.graph.ContainsEdge(action))
            {
                throw new ActionNotFoundException(action);
            }
        }

        protected ActionEdge VerifyActionExistsDuringConfigChange(string action, State s, State t)
        {
            this.graph.TryGetEdges(s, t, out var edges);

            if (edges == null)
            {
                throw new ActionNotFoundException(action, s, t);
            }

            var edge = edges.SingleOrDefault(x => x.Name == action);

            if (edge == null)
            {
                throw new ActionNotFoundException(action, s, t);
            }

            return (ActionEdge)edge;
        }

        protected virtual State OnCreateWorkflowState(string name)
        {
            State newState;

            if (string.IsNullOrWhiteSpace(name))
            {
                newState = State.NullState;
            }
            else
            {
                newState = new State(name, Interlocked.Increment(ref this.stateCounter), false);
            }

            this.graph.AddVertex(newState);

            return newState;
        }

        protected State FindWorkflowState(string name)
        {
            if (name == null)
            {
                name = string.Empty;
            }

            return (State)this.graph.Vertices.SingleOrDefault(v => string.Equals(v.Name, name, StringComparison.InvariantCultureIgnoreCase));
        }

        protected State FindOrCreateWorkflowState(string name)
        {
            var state = this.FindWorkflowState(name);

            if (state == null)
            {
                state = this.OnCreateWorkflowState(name);
            }

            return state;
        }

        protected State FindWorkflowState(long id)
        {
            return this.graph.Vertices.SingleOrDefault(v => v.Id == id) as State;
        }

        protected ActionEdge FindWorkflowAction(long id)
        {
            return this.graph.Edges.SingleOrDefault(e => e.Id == id) as ActionEdge;
        }
    }
}
