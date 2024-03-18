// -----------------------------------------------------------------------
// <copyright file="CompleteHeapTree.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
#if NET6_0_OR_GREATER
namespace Abbotware.Core.Collections.Trees
{
    using System;
    using Abbotware.Core.Collections.Trees.Internal;

    /// <summary>
    /// Complete Heap Base Tree
    /// </summary>
    /// <typeparam name="TNode">node type</typeparam>
    public class CompleteHeapTree<TNode> : BaseTree<TNode>
        where TNode : class, ITreeNode<TNode>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteHeapTree{T}"/> class.
        /// </summary>
        /// <param name="branches">number of branches per node</param>
        /// <param name="height">height of tree</param>
        public CompleteHeapTree(ushort branches, uint height)
            : base(branches, height)
        {
        }

        /// <inheritdoc/>
        public override ulong OnComputeNodeCount(uint height)
        {
            return (ulong)(Math.Pow(this.Branches, height + 1d) - 1d) / (ulong)(this.Branches - 1d);
        }

        /// <inheritdoc/>
        public override ushort OnComputeDepth(ulong position)
        {
            var log2 = Math.Log2(position + 1);
            var floor = (uint)Math.Floor(log2);
            var height = floor;

            return (ushort)height;
        }

        /// <inheritdoc/>
        public override ulong OnComputeChildIndex(ulong position, ushort child)
        {
            if (child >= this.Branches)
            {
                throw new ArgumentOutOfRangeException($"child:{child} but there are only {this.Branches} branches");
            }

            return (this.Branches * position) + (ulong)(child + 1);
        }

        /// <inheritdoc/>
        public override ulong[] OnComputeParentIndices(ulong position)
        {
            return new[] { (position - 1) / this.Branches };
        }
    }
}
#endif