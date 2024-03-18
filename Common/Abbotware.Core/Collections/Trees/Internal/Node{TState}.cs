// -----------------------------------------------------------------------
// <copyright file="Node{TState}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
#if NET6_0_OR_GREATER
namespace Abbotware.Core.Collections.Trees.Internal
{
    /// <summary>
    /// Basic Node with State Variable
    /// </summary>
    /// <typeparam name="TState">State Variable Type</typeparam>
    public class Node<TState> : Node<Node<TState>, TState>
        where TState : new()
    {
    }
}
#endif