// -----------------------------------------------------------------------
// <copyright file="TreeCollection{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    ///     Tree based collection
    /// </summary>
    /// <typeparam name="T">type of data stored in tree</typeparam>
    public class TreeCollection<T> : IEnumerable<T>
    {
        /// <summary>
        ///     internal list of children
        /// </summary>
        private readonly List<TreeCollection<T>> children = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeCollection{T}"/> class.
        /// </summary>
        /// <param name="data">data for node</param>
        public TreeCollection(T data)
        {
            this.Data = data;

            this.NodeIndex = new List<TreeCollection<T>> { this };
        }

        /// <summary>
        ///     Gets or sets the data assocaiated with the tree node
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        ///     Gets or sets the of the tree
        /// </summary>
        public TreeCollection<T>? Parent { get; set; }

        /// <summary>
        ///     Gets the children of the tree
        /// </summary>
        public ICollection<TreeCollection<T>> Children => this.children;

        /// <summary>
        ///     Gets a value indicating whether or not the tree is the root node
        /// </summary>
        public bool IsRoot => this.Parent == null;

        /// <summary>
        ///     Gets a value indicating whether or not the tree a leaf node
        /// </summary>
        public bool IsLeaf => this.Children.Count == 0;

        /// <summary>
        ///     Gets the level of the tree node
        /// </summary>
        public int Level => this.IsRoot ? 0 : this.Parent!.Level + 1;

        /// <summary>
        /// Gets the List of all tree nodes
        /// </summary>
        private List<TreeCollection<T>> NodeIndex { get; }

        /// <inheritdoc cref="IEnumerable" />
        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        /// <inheritdoc cref="IEnumerable" />
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            yield return this.Data;

            foreach (var directChild in this.Children)
            {
                foreach (T anyChild in directChild)
                {
                    yield return anyChild;
                }
            }
        }

        /// <summary>
        /// Adds a child to the current tree node
        /// </summary>
        /// <param name="childData">child data</param>
        /// <returns>node containing child data</returns>
        public TreeCollection<T> AddChild(T childData)
        {
            var childNode = new TreeCollection<T>(childData) { Parent = this };

            this.Children.Add(childNode);

            this.RegisterChildForSearch(childNode);

            return childNode;
        }

        /// <inheritdoc cref="object" />
        public override string ToString()
        {
            return this.Data != null ? this.Data.ToString() : "[data null]";
        }

        /// <summary>
        /// finds the first node in the tree that matches predicate
        /// </summary>
        /// <param name="predicate">search predicate</param>
        /// <returns>tree node (if any)</returns>
        public TreeCollection<T>? FirstOrDefault(Func<T, bool> predicate)
        {
            return this.NodeIndex.FirstOrDefault(x => predicate(x.Data));
        }

        /// <summary>
        /// Adds a tree node to the search structure
        /// </summary>
        /// <param name="node">node to register</param>
        private void RegisterChildForSearch(TreeCollection<T> node)
        {
            this.NodeIndex.Add(node);

            this.Parent?.RegisterChildForSearch(node);
        }
    }
}