// -----------------------------------------------------------------------
// <copyright file="ILocalAndRemote{TLocal,TRemote}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache
{
    /// <summary>
    /// interface for local and remote store
    /// </summary>
    /// <typeparam name="TLocal">local type</typeparam>
    /// <typeparam name="TRemote">remote type</typeparam>
    public interface ILocalAndRemote<TLocal, TRemote>
    {
        /// <summary>
        ///  Gets the local store
        /// </summary>
        TLocal Local { get; }

        /// <summary>
        /// Gets the remote store
        /// </summary>
        TRemote Remote { get; }
    }
}