// -----------------------------------------------------------------------
// <copyright file="Node{TNode,TState}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

#if NET6_0_OR_GREATER
namespace Abbotware.Core.Collections.Trees.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Tree Node with a state variable
    /// </summary>
    /// <typeparam name="TNode">Tree Node Type (for type forwarding)</typeparam>
    /// <typeparam name="TState">State Type</typeparam>
    public class Node<TNode, TState> : ITreeNode<TNode>
        where TNode : Node<TNode, TState>
        where TState : new()
    {
        /// <inheritdoc/>
        [JsonIgnore]
        public IReadOnlyList<TNode> Parents { get; set; } = Array.Empty<TNode>();

        /// <inheritdoc/>
        [JsonIgnore]
        public IReadOnlyList<TNode> Children { get; set; } = Array.Empty<TNode>();

        /// <inheritdoc/>
        public ulong Id { get; set; }

        /// <inheritdoc/>
        public IReadOnlyList<ulong> ParentIds { get; set; } = Array.Empty<ulong>();

        /// <inheritdoc/>
        public IReadOnlyList<ulong> ChildIds { get; set; } = Array.Empty<ulong>();

        /// <inheritdoc/>
        public uint Depth { get; set; }

        /// <summary>
        /// Gets or sets the State variable
        /// </summary>
        public TState State { get; set; } = new();
    }
}
#endif