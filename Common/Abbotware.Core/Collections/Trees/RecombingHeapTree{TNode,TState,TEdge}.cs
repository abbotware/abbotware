// -----------------------------------------------------------------------
// <copyright file="RecombingHeapTree{TNode,TState,TEdge}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
#if NET6_0_OR_GREATER
namespace Abbotware.Core.Collections.Trees
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using Abbotware.Core.Collections.Trees.Internal;

    /// <summary>
    /// tree with edges
    /// </summary>
    /// <typeparam name="TNode">node type</typeparam>
    /// <typeparam name="TState">state variable</typeparam>
    /// <typeparam name="TEdge">edge type</typeparam>
    public class RecombingHeapTree<TNode, TState, TEdge> : RecombingHeapTree<TNode, TState>
    where TNode : Node<TNode, TState>, new()
    where TState : new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecombingHeapTree{TNode, TState, TEdge}"/> class.
        /// </summary>
        /// <param name="branches">number of branches per node</param>
        /// <param name="height">height of tree</param>
        public RecombingHeapTree(ushort branches, uint height)
            : base(branches, height)
        {
        }

        /// <summary>
        ///  gets the edges
        /// </summary>
        public Dictionary<string, TEdge> Edges { get; } = new();
    }
}
#endif