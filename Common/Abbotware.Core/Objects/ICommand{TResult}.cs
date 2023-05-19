// -----------------------------------------------------------------------
// <copyright file="ICommand{TResult}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for a command
    /// </summary>
    /// <typeparam name="TResult">command result type</typeparam>
    public interface ICommand<TResult>
    {
        /// <summary>
        /// executes the command asynchronous
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        Task<TResult> ExecuteAsync(CancellationToken ct);
    }
}