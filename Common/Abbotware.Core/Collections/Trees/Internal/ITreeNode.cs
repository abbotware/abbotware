// -----------------------------------------------------------------------
// <copyright file="ITreeNode.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Collections.Trees.Internal
{
    using System.Collections.Generic;

    /// <summary>
    /// Tree Node interface
    /// </summary>
    public interface ITreeNode
    {
        /// <summary>
        /// Gets or sets the Id of the tree node
        /// </summary>
        ulong Id { get; internal set; }

        /// <summary>
        /// Gets or sets the Parent Node Id (or Parent Node Ids if this is a recombining trees)
        /// </summary>
        /// <remarks>no parent ids means the node is the root node</remarks>
        IReadOnlyList<ulong> ParentIds { get; internal set; }

        /// <summary>
        /// Gets or sets the Child Node Ids
        /// </summary>
        /// <remarks>no child node ids means the node is leaf node</remarks>
        IReadOnlyList<ulong> ChildIds { get; internal set; }

        /// <summary>
        /// Gets or sets the depth of the node in the tree
        /// </summary>
        uint Depth { get; internal set; }
    }
}
