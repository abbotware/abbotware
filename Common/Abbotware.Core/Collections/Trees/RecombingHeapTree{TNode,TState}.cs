// -----------------------------------------------------------------------
// <copyright file="RecombingHeapTree{TNode,TState}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
#if NET6_0_OR_GREATER
namespace Abbotware.Core.Collections.Trees
{
    using System;
    using System.Collections.Generic;
    using Abbotware.Core.Collections.Trees.Internal;

    /// <summary>
    /// Recombining Heap Based Tree
    /// </summary>
    /// <typeparam name="TNode">node type</typeparam>
    /// <typeparam name="TState">state variable</typeparam>
    public class RecombingHeapTree<TNode, TState> : BaseTree<TNode>
        where TNode : Node<TNode, TState>, new()
        where TState : new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecombingHeapTree{TNode, TState}"/> class.
        /// </summary>
        /// <param name="branches">number of branches per node</param>
        /// <param name="height">height of tree</param>
        public RecombingHeapTree(ushort branches, uint height)
            : base(branches, height)
        {
        }

        /// <inheritdoc/>
        public override ulong OnComputeNodeCount(uint height)
        {
            return this.Branches switch
            {
                2 => height * (height + 1) / 2,
                3 => (ulong)height,
                _ => throw new NotSupportedException($"Branches:{this.Branches} unsupported"),
            };
        }

        /// <inheritdoc/>
        public override ushort OnComputeDepth(ulong position)
        {
            return this.Branches switch
            {
                2 => Depth2Branches(position),
                _ => throw new NotSupportedException($"Branches:{this.Branches} unsupported"),
            };

            ushort Depth2Branches(ulong position)
            {
                // https://math.stackexchange.com/questions/672433/finding-the-parent-of-a-node-in-recombining-binomial-tree
                var n = Math.Ceiling(.5 * (-1 + Math.Sqrt(1 + (8 * (position + 1)))));
                return (ushort)(n - 1);
            }
        }

        /// <inheritdoc/>
        public override ulong OnComputeChildIndex(ulong position, ushort child)
        {
            if (child >= this.Branches)
            {
                throw new ArgumentOutOfRangeException($"child:{child} but there are only {this.Branches} branches");
            }

            return this.Branches switch
            {
                // https://math.stackexchange.com/questions/668795/finding-the-child-node-in-the-recombining-binomial-tree
                2 => position + this.OnComputeDepth(position) + child + 1,
                3 => (3 * position) + (ulong)(child - 2),
                _ => throw new NotSupportedException($"Branches:{this.Branches} unsupported"),
            };
        }

        /// <inheritdoc/>
        public override ulong[] OnComputeParentIndices(ulong position)
        {
            return this.Branches switch
            {
                2 => Parents2Branches(position),
                ////3 => (long)height,
                _ => throw new NotSupportedException($"Branches:{this.Branches} unsupported"),
            };

            ulong[] Parents2Branches(ulong position)
            {
                List<ulong> parents = new List<ulong>();

                // https://math.stackexchange.com/questions/672433/finding-the-parent-of-a-node-in-recombining-binomial-tree
                var d = this.OnComputeDepth(position);
                var p1 = position - d;
                var p2 = position - d + 1;

                if (position != .5 * (d * (d + 1)))
                {
                    parents.Add(p1);
                }

                if (position != .5 * (d - 1) * (d + 1))
                {
                    parents.Add(p2);
                }

                return parents.ToArray();
            }
        }
    }
}
#endif