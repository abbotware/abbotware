// -----------------------------------------------------------------------
// <copyright file="ISmtpManagerOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Net.Configuration
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    /// <summary>
    /// read only smtp manager options
    /// </summary>
    public interface ISmtpManagerOptions
    {
        /// <summary>
        ///     Gets the credential for the SMTP server
        /// </summary>
        NetworkCredential? Credential { get;  }

        /// <summary>
        ///     Gets the DeliveryMethod type for the SMTP Manger
        /// </summary>
        SmtpDeliveryMethod DeliveryMethod { get; }

        /// <summary>
        ///     Gets the DNS endpoint for SMTP server
        /// </summary>
        DnsEndPoint? EndPoint { get; }

        /// <summary>
        ///     Gets the From address when sending messages
        /// </summary>
        MailAddress From { get; }

        /// <summary>
        ///     Gets the message encoding used when sending messages
        /// </summary>
        Encoding MessageEncoding { get; }

        /// <summary>
        ///     Gets the Pick Directory when Delivery Method = SpecifiedPickupDirectory
        /// </summary>
        Uri? PickupDirectory { get; }

        /// <summary>
        ///     Gets a value indicating whether or not the SMTP configuration uses SSL
        /// </summary>
        bool Ssl { get;  }
    }
}