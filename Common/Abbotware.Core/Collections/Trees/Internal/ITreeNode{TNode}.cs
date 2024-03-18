// -----------------------------------------------------------------------
// <copyright file="ITreeNode{TNode}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Collections.Trees.Internal
{
    using System.Collections.Generic;

    /// <summary>
    /// Tree node Interface with typed references to parent(s)/children nodes
    /// </summary>
    /// <typeparam name="TNode">node type</typeparam>
    public interface ITreeNode<TNode> : ITreeNode
        where TNode : ITreeNode
    {
        /// <summary>
        /// Gets or sets the parent node (or Parent Nodes if this is a recombining trees)
        /// </summary>
        IReadOnlyList<TNode> Parents { get; internal set; }

        /// <summary>
        /// Gets or sets the Children Nodes
        /// </summary>
        IReadOnlyList<TNode> Children { get; internal set; }
    }
}
