// -----------------------------------------------------------------------
// <copyright file="BaseTree{TNode}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
#if NET6_0_OR_GREATER
namespace Abbotware.Core.Collections.Trees.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    /// <summary>
    /// base class for a tree data structure
    /// </summary>
    /// <typeparam name="TNode">tree node type</typeparam>
    public abstract class BaseTree<TNode>
        where TNode : class, ITreeNode<TNode>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTree{TNode}"/> class.
        /// </summary>
        /// <param name="branches">number of branches per node</param>
        /// <param name="height">height of tree</param>
        protected BaseTree(ushort branches, uint height)
        {
            this.Branches = branches;
            this.Height = height;

            var length = this.OnComputeNodeCount(height);
            var nodes = new TNode[length];
            this.Nodes = nodes;

            for (ulong i = 0; i < length; i++)
            {
                var n = new TNode();
                n.Id = i;
                nodes[i] = n;
            }

            var levelsLookup = new ConcurrentDictionary<uint, HashSet<ulong>>();
            var childrenLookup = new ConcurrentDictionary<ulong, HashSet<ulong>>();
            var parentsLookup = new ConcurrentDictionary<ulong, HashSet<ulong>>();

            for (ulong i = 0; i < length; i++)
            {
                var n = nodes[i];
                n.Depth = this.OnComputeDepth(i);
                var lvl = levelsLookup.GetOrAdd(n.Depth, _ => new HashSet<ulong>());
                lvl.Add(n.Id);

                if (n.Depth < this.Height - 1)
                {
                    var childIds = new ulong[this.Branches];
                    n.ChildIds = childIds;
                    var childrenNodes = new TNode[this.Branches];
                    n.Children = childrenNodes;

                    for (ushort b = 0; b < branches; b++)
                    {
                        var index = this.OnComputeChildIndex(i, b);
                        childIds[b] = index;
                        childrenNodes[b] = nodes[n.ChildIds[b]];

                        n.Children[b].ParentIds = n.Children[b].ParentIds.Union([i]).ToArray();
                        n.Children[b].Parents = n.Children[b].Parents.Union([n]).ToArray();

                        var p = parentsLookup.GetOrAdd(index, _ => new HashSet<ulong>());
                        var c = childrenLookup.GetOrAdd(i, _ => new HashSet<ulong>());
                        c.Add(index);
                        p.Add(i);
                    }
                }
            }

            this.Levels = levelsLookup.ToDictionary(kvp => kvp.Key, kvp => (IReadOnlySet<ulong>)kvp.Value.ToImmutableHashSet()).ToImmutableDictionary();
            this.Children = childrenLookup.ToDictionary(kvp => kvp.Key, kvp => (IReadOnlySet<ulong>)kvp.Value.ToImmutableHashSet()).ToImmutableDictionary();
            this.Parents = parentsLookup.ToDictionary(kvp => kvp.Key, kvp => (IReadOnlySet<ulong>)kvp.Value.ToImmutableHashSet()).ToImmutableDictionary();
        }

        /// <summary>
        /// Gets the nodes
        /// </summary>
        public IReadOnlyList<TNode> Nodes { get; }

        /// <summary>
        /// Gets the root node
        /// </summary>
        public TNode Root => this.Nodes[0];

        /// <summary>
        /// Gets the number of branches per node
        /// </summary>
        public ushort Branches { get; }

        /// <summary>
        /// Gets the tree height
        /// </summary>
        public uint Height { get; }

        /// <summary>
        /// Gets the Level -> Node Ids mapping
        /// </summary>
        public IReadOnlyDictionary<uint, IReadOnlySet<ulong>> Levels { get; }

        /// <summary>
        /// Gets the Node Id -> Children mapping
        /// </summary>
        protected IReadOnlyDictionary<ulong, IReadOnlySet<ulong>> Children { get; }

        /// <summary>
        /// Gets the Node Id -> Parents mapping
        /// </summary>
        protected IReadOnlyDictionary<ulong, IReadOnlySet<ulong>> Parents { get; }

        /// <summary>
        /// gets the node by id
        /// </summary>
        /// <param name="nodeId">Node Id</param>
        /// <returns>node</returns>
        public TNode this[ulong nodeId] => this.Nodes[(int)nodeId];

        /// <summary>
        /// Upward Walk
        /// </summary>
        /// <param name="onLeafNode">callback for leaf node visitor</param>
        /// <param name="onParentNode">callback for parent node visitor</param>
        public void TraverseUp(Action<TNode> onLeafNode, Action<TNode> onParentNode)
        {
            var levels = this.Levels.Keys.OrderByDescending(a => a).ToList();

            foreach (var h in levels.Take(1))
            {
                foreach (var l in this.Levels[h])
                {
                    onLeafNode(this.Nodes[(int)l]);
                }
            }

            foreach (var h in levels.Skip(1))
            {
                foreach (var l in this.Levels[h])
                {
                    onParentNode(this.Nodes[(int)l]);
                }
            }
        }

        /// <summary>
        /// Downward walk - semi-optimized for recombining trees
        /// </summary>
        /// <param name="onRootNode">callback for root node visitor</param>
        /// <param name="onInteriorNode">callback for interior node visitor</param>
        /// <param name="onLeafNode">callback for leaf node visitor</param>
        public void TraverseDown(Action<TNode> onRootNode, Action<TNode> onInteriorNode, Action<TNode> onLeafNode)
        {
            var visited = new HashSet<ulong>();
            var found = new HashSet<ulong>();

            var walk = new Queue<ulong>();

            // node 0 = root node
            walk.Enqueue(0);

            onRootNode(this.Nodes[0]);

            while (walk.Count != 0)
            {
                var t = walk.Dequeue();
                visited.Add(t);
                var n = this.Nodes[(int)t];

                if (n.ChildIds.Any())
                {
                    onInteriorNode(n);

                    foreach (var c in this.Nodes[(int)t].ChildIds)
                    {
                        if (!visited.Contains(c) && !found.Contains(c))
                        {
                            walk.Enqueue(c);
                            found.Add(c);
                        }
                    }
                }
                else
                {
                    onLeafNode(n);
                }
            }
        }

        /// <summary>
        /// Downward walk - semi-optimized for recombining trees
        /// </summary>
        /// <param name="onRootNode">callback for root node visitor</param>
        /// <param name="onInteriorNode">callback for interior node visitor</param>
        /// <param name="onLeafNode">callback for leaf node visitor</param>
        public void TraverseDownDepthFirst(Action<TNode> onRootNode, Action<TNode> onInteriorNode, Action<TNode> onLeafNode)
        {
            var visited = new HashSet<ulong>();
            var found = new HashSet<ulong>();

            var walk = new Stack<ulong>();

            // node 0 = root node
            walk.Push(0);

            onRootNode(this.Nodes[0]);

            while (walk.Count != 0)
            {
                var t = walk.Pop();
                visited.Add(t);
                var n = this.Nodes[(int)t];

                if (n.ChildIds.Any())
                {
                    onInteriorNode(n);

                    foreach (var c in this.Nodes[(int)t].ChildIds)
                    {
                        if (!visited.Contains(c) && !found.Contains(c))
                        {
                            walk.Push(c);
                            found.Add(c);
                        }
                    }
                }
                else
                {
                    onLeafNode(n);
                }
            }
        }


        /// <summary>
        /// Downward walk - semi-optimized for recombining trees
        /// </summary>
        /// <param name="onRootNode">callback for root node visitor</param>
        /// <param name="onMiddleNode"></param>
        /// <param name="onUpperInteriorNode"></param>
        /// <param name="onLowerInteriorNode"></param>
        /// <param name="onUpperFroniterNode"></param>
        /// <param name="onLowerFroniterNode"></param>
        /// <param name="onInteriorNode">callback for interior node visitor</param>
        /// <param name="onLeafNode">callback for leaf node visitor</param>
        public void TraverseDownMiddleOut(Action<TNode> onRootNode, Action<TNode> onMiddleNode, Action<TNode> onUpperInteriorNode, Action<TNode> onLowerInteriorNode, Action<TNode> onUpperFrontierNode, Action<TNode> onLowerFrontierNode)
        {
            for (uint i = 0; i < this.Levels.Count; ++i)
            {
                if (i == 0)
                {
                    onRootNode(this.Nodes[0]);
                    continue;
                }

                var (lower, middle, upper) = SplitSortedList(this.Levels[i].ToList());

                if (middle is not null)
                {
                    onMiddleNode(this.Nodes[(int)middle.Value]);
                }

                var u = 0;
                for (u = 0; u < upper.Count - 1; ++u)
                {
                    onUpperInteriorNode(this.Nodes[(int)upper[u]]);
                }

                onUpperFrontierNode(this.Nodes[(int)upper[u]]);

                var l = 0;
                for (l = 0; l < lower.Count - 1; ++l)
                {
                    onLowerInteriorNode(this.Nodes[(int)lower[l]]);
                }

                onLowerFrontierNode(this.Nodes[(int)lower[l]]);
            }

            static (IReadOnlyList<ulong> Lower, ulong? Middle, IReadOnlyList<ulong> Upper) SplitSortedList(IReadOnlyList<ulong> list)
            {
                int n = list.Count;
                int midpoint = n / 2;

                List<ulong> lowerHalf;
                ulong? middle;
                List<ulong> upperHalf;

                if (n % 2 == 1)
                {
                    // Odd length: single middle element
                    lowerHalf = new List<ulong>(list.Take(midpoint));
                    middle = list[midpoint];
                    upperHalf = new List<ulong>(list.Skip(midpoint + 1));
                }
                else
                {
                    // Even length: no middle element
                    lowerHalf = new List<ulong>(list.Take(midpoint));
                    middle = null;
                    upperHalf = new List<ulong>(list.Skip(midpoint));
                }

                lowerHalf.Reverse();
                return (lowerHalf, middle, upperHalf);
            }
        }


        /// <summary>
        /// Computes the node count for tree of given a height
        /// </summary>
        /// <param name="height">tree height</param>
        /// <returns>number of nodes to create</returns>
        public abstract ulong OnComputeNodeCount(uint height);

        /// <summary>
        /// Callback logic to compute the depth of a given position in the nodes array
        /// </summary>
        /// <param name="position">position offset into the nodes array</param>
        /// <returns>depth of the node in the tree</returns>
        public abstract ushort OnComputeDepth(ulong position);

        /// <summary>
        /// Callback logic to compute the child index of a given position in the nodes array
        /// </summary>
        /// <param name="position">position offset into the nodes array</param>
        /// <param name="child">child branch number</param>
        /// <returns>position offset into the nodes array for the child</returns>
        public abstract ulong OnComputeChildIndex(ulong position, ushort child);

        /// <summary>
        /// Callback logic to compute the parent(s) indices of a given position in the nodes array
        /// </summary>
        /// <param name="position">position offset into the nodes array</param>
        /// <returns>position offset into the nodes array for node's parents</returns>
        public abstract ulong[] OnComputeParentIndices(ulong position);
    }
}
#endif