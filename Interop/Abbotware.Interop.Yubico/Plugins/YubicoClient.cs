// -----------------------------------------------------------------------
// <copyright file="YubicoClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Yubico.Plugins
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.Yubico;
    using Microsoft.Extensions.Logging;
    using YubicoDotNetClient;

    /// <summary>
    /// Yubico Client
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="YubicoClient"/> class.
    /// </remarks>
    /// <param name="credential">api credential</param>
    /// <param name="logger">injected logger</param>
    public class YubicoClient(NetworkCredential credential, ILogger logger)
        : BaseComponent<NetworkCredential>(credential, logger), IYubicoClient
    {
        /// <inheritdoc/>
        public async Task<bool> VerifyAsync(string otp, CancellationToken ct)
        {
            var client = new YubicoDotNetClient.YubicoClient(this.Configuration.UserName, this.Configuration.Password);

            try
            {
                var response = await client.VerifyAsync(otp)
                    .ConfigureAwait(false);

                if (response?.Status == YubicoResponseStatus.Ok)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex, "Yubico Exception");
            }

            return false;
        }
    }
}