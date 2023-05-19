// -----------------------------------------------------------------------
// <copyright file="ActionEdge.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow.Plugins
{
    using Abbotware.Core.Workflow.ExtensionPoints;

    /// <summary>
    /// Workflow Action
    /// </summary>
    public class ActionEdge : BaseWorkflowComponent, IAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionEdge"/> class.
        /// </summary>
        /// <param name="name">action name</param>
        /// <param name="id">action id</param>
        /// <param name="source">source state</param>
        /// <param name="target">target state</param>
        public ActionEdge(string name, long id, IState source, IState target)
            : base(name, id)
        {
            this.Source = source;
            this.Target = target;
        }

        /// <inheritdoc/>
        public IState Source { get; }

        /// <inheritdoc/>
        public IState Target { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[{this.Name}:({this.Source.Name})->({this.Target.Name})] = {this.Id}";
        }
    }
}
