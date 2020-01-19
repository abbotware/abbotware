// -----------------------------------------------------------------------
// <copyright file="ISmtpManager.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Net
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     Interface for sending email messages
    /// </summary>
    public interface ISmtpManager
    {
        /// <summary>
        ///     Sends an Email message
        /// </summary>
        /// <param name="recipient">recpient's email</param>
        /// <param name="subject">message subject</param>
        /// <param name="body">message body</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        Task SendAsync(string recipient, string subject, string body, CancellationToken ct);

        /// <summary>
        ///     Sends an Email message in HTML format
        /// </summary>
        /// <param name="recipients">list of email addresses</param>
        /// <param name="subject">message subject</param>
        /// <param name="body">message body</param>
        /// <param name="isHtml">true if body is in HTML</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        Task SendAsync(IEnumerable<string> recipients, string subject, string body, bool isHtml, CancellationToken ct);

        /// <summary>
        ///     Sends an Email message
        /// </summary>
        /// <param name="recipients">list of email addresses</param>
        /// <param name="subject">message subject</param>
        /// <param name="body">message body</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        Task SendAsync(IEnumerable<string> recipients, string subject, string body, CancellationToken ct);

        /// <summary>
        ///     Sends an Email message with attachments
        /// </summary>
        /// <param name="recipients">list of email addresses</param>
        /// <param name="subject">message subject</param>
        /// <param name="body">message body</param>
        /// <param name="attachments">file paths to include as attachement</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        Task SendAsync(IEnumerable<string> recipients, string subject, string body, IEnumerable<Uri> attachments, CancellationToken ct);

        /// <summary>
        ///     Sends an email message
        /// </summary>
        /// <param name="recipients">list of recipients</param>
        /// <param name="subject">email subject</param>
        /// <param name="body">email body</param>
        /// <param name="attachments">list of file attachments</param>
        /// <param name="isHtml">flag indicating body is html</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        Task SendAsync(IEnumerable<string> recipients, string subject, string body, IEnumerable<Uri> attachments, bool isHtml, CancellationToken ct);
    }
}