// -----------------------------------------------------------------------
// <copyright file="State.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow.Plugins
{
    using Abbotware.Core.Workflow.ExtensionPoints;

    public class State : BaseWorkflowComponent, IState
    {
        public static readonly State NullState = new State(string.Empty, 0, false);

        public State(string name, long id, bool isStart)
            : base(name, id)
        {
            this.IsStart = isStart;
        }

        public bool IsStart { get; }

        public override string ToString()
        {
            return $"({this.Name}) = {this.Id}]";
        }
    }
}
