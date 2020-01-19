// -----------------------------------------------------------------------
// <copyright file="ApiClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Yubico.Plugins
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Abbotware.Core.Logging;
    using Abbotware.Interop.Yubico.ExtensionPoints;
    using YubicoDotNetClient;

    /// <summary>
    /// Yubico Client
    /// </summary>
    public class ApiClient : IApiClient
    {
        /// <summary>
        /// yubic api client
        /// </summary>
        private readonly NetworkCredential credential;

        /// <summary>
        /// injected logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient"/> class.
        /// </summary>
        /// <param name="credential">api credential</param>
        /// <param name="logger">injected logger</param>
        public ApiClient(NetworkCredential credential, ILogger logger)
        {
            this.credential = credential;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public async Task<bool> VerifyAsync(string otp)
        {
            var client = new YubicoClient(this.credential.UserName, this.credential.Password);

            try
            {
                var response = await client.VerifyAsync(otp).ConfigureAwait(false);

                if (response?.Status == YubicoResponseStatus.Ok)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Yubico Exception");
            }

            return false;
        }
    }
}