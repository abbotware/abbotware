// -----------------------------------------------------------------------
// <copyright file="RecombingHeapTree{TState}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
#if NET6_0_OR_GREATER
namespace Abbotware.Core.Collections.Trees
{
    using Abbotware.Core.Collections.Trees.Internal;

    /// <summary>
    /// Recombining Heap Based Tree
    /// </summary>
    /// <typeparam name="TState">state variable</typeparam>
    public class RecombingHeapTree<TState> : RecombingHeapTree<Node<TState>, TState>
    where TState : new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecombingHeapTree{TState}"/> class.
        /// </summary>
        /// <param name="branches">number of branches per node</param>
        /// <param name="height">height of tree</param>
        public RecombingHeapTree(ushort branches, uint height)
            : base(branches, height)
        {
        }
    }
}
#endif