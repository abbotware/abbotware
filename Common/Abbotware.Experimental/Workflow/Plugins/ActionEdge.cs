// -----------------------------------------------------------------------
// <copyright file="ActionEdge.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow.Plugins
{
    using Abbotware.Core.Workflow.ExtensionPoints;

    public class ActionEdge : BaseWorkflowComponent, IAction
    {
        public ActionEdge(string name, long id, IState source, IState target)
            : base(name, id)
        {
            this.Source = source;
            this.Target = target;
        }

        public IState Source { get; }

        public IState Target { get; }

        public override string ToString()
        {
            return $"[{this.Name}:({this.Source.Name})->({this.Target.Name})] = {this.Id}";
        }
    }
}
