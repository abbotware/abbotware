// -----------------------------------------------------------------------
// <copyright file="SmtpManager.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Net.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Net;
    using Abbotware.Core.Net.Configuration;
    using Abbotware.Core.Objects;

    /// <summary>
    ///     Manager class for sending email
    /// </summary>
    public class SmtpManager : BaseComponent<ISmtpManagerOptions>, ISmtpManager
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SmtpManager" /> class.
        /// </summary>
        /// <param name="config">configuration for smtp manager</param>
        /// <param name="logger">injected logger</param>
        public SmtpManager(ISmtpManagerOptions config, ILogger logger)
            : base(config, logger)
        {
            Arguments.NotNull(config, nameof(config));
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <inheritdoc />
        public Task SendAsync(string recipient, string subject, string body, CancellationToken ct)
        {
            this.ThrowIfDisposed();

            return this.SendAsync(new string[1] { recipient }, subject, body, ct);
        }

        /// <inheritdoc />
        public Task SendAsync(IEnumerable<string> recipients, string subject, string body, CancellationToken ct)
        {
            this.ThrowIfDisposed();

            return this.SendAsync(recipients, subject, body, Enumerable.Empty<Uri>(), ct);
        }

        /// <inheritdoc />
        public Task SendAsync(IEnumerable<string> recipients, string subject, string body, bool isHtml, CancellationToken ct)
        {
            this.ThrowIfDisposed();

            return this.SendAsync(recipients, subject, body, Enumerable.Empty<Uri>(), isHtml, ct);
        }

        /// <inheritdoc />
        public Task SendAsync(IEnumerable<string> recipients, string subject, string body, IEnumerable<Uri> attachments, CancellationToken ct)
        {
            this.ThrowIfDisposed();

            return this.SendAsync(recipients, subject, body, attachments, false, ct);
        }

        /// <inheritdoc />
        public Task SendAsync(IEnumerable<string> recipients, string subject, string body, IEnumerable<Uri> attachments, bool isHtml, CancellationToken ct)
        {
            recipients = Arguments.EnsureNotNull(recipients, nameof(recipients));
            attachments = Arguments.EnsureNotNull(attachments, nameof(attachments));

            this.ThrowIfDisposed();

            this.Logger.Info($"Sending Email:{subject} to:{string.Join(",", recipients)} isHtml:{isHtml}");

            return SmtpManager.SendAsync(this.Configuration, recipients, subject, body, attachments, isHtml, ct);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SmtpManager" /> class.
        /// </summary>
        /// <param name="config">configuration for smtp manager</param>
        /// <param name="recipients">list of recipients</param>
        /// <param name="subject">email subject</param>
        /// <param name="body">email body</param>
        /// <param name="attachments">list of file attachments</param>
        /// <param name="isHtml">flag indicating body is html</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        private static async Task SendAsync(ISmtpManagerOptions config, IEnumerable<string> recipients, string subject, string body, IEnumerable<Uri> attachments, bool isHtml, CancellationToken ct)
        {
            Arguments.NotNull(config, nameof(config));
            Arguments.NotNull(recipients, nameof(recipients));
            Arguments.NotNull(subject, nameof(subject));
            Arguments.NotNull(body, nameof(body));
            Arguments.NotNull(attachments, nameof(attachments));

            using var client = new SmtpClient();

            if (config.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory)
            {
                var dir = config.PickupDirectory!;
                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.PickupDirectoryLocation = dir.LocalPath;
            }
            else
            {
                var endpoint = config.EndPoint!;

                client.Host = endpoint.Host;
                client.Port = endpoint.Port;
                client.UseDefaultCredentials = false;
                client.Credentials = config.Credential;
                client.EnableSsl = config.Ssl;
            }

            using var message = new MailMessage
            {
                From = config.From,
            };

            foreach (var recipient in recipients)
            {
                message.To.Add(new MailAddress(recipient));
            }

            message.Body = body;
            message.BodyEncoding = config.MessageEncoding;
            message.Subject = subject;
            message.SubjectEncoding = config.MessageEncoding;
            message.IsBodyHtml = isHtml;

            foreach (var file in attachments)
            {
                message.Attachments.Add(new Attachment(file.LocalPath));
            }

#if NET6_0_OR_GREATER
            await client.SendMailAsync(message, ct)
                .ConfigureAwait(false);
#else
            GC.KeepAlive(ct);

            await client.SendMailAsync(message)
                .ConfigureAwait(false);
#endif
        }
    }
}