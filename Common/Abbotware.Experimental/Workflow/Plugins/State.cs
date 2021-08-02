// -----------------------------------------------------------------------
// <copyright file="State.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow.Plugins
{
    using Abbotware.Core.Workflow.ExtensionPoints;

    /// <summary>
    /// Workflow state
    /// </summary>
    public class State : BaseWorkflowComponent, IState
    {
        /// <summary>
        /// global Null state instance
        /// </summary>
        public static readonly State NullState = new State(string.Empty, 0, false);

        /// <summary>
        /// Initializes a new instance of the <see cref="State"/> class.
        /// </summary>
        /// <param name="name">state name</param>
        /// <param name="id">state id</param>
        /// <param name="isStart">start flag</param>
        public State(string name, long id, bool isStart)
            : base(name, id)
        {
            this.IsStart = isStart;
        }

        /// <inheritdoc/>
        public bool IsStart { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"({this.Name}) = {this.Id}]";
        }
    }
}
