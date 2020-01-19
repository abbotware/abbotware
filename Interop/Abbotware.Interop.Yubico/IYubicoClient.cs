// -----------------------------------------------------------------------
// <copyright file="IYubicoClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Yubico.ExtensionPoints
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// interface with Yubico Api Client
    /// </summary>
    public interface IYubicoClient
    {
        /// <summary>
        /// verify the one time password
        /// </summary>
        /// <param name="otp">one time password</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        Task<bool> VerifyAsync(string otp, CancellationToken ct);
    }
}