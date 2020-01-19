// -----------------------------------------------------------------------
// <copyright file="SmtpManagerOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Net.Configuration.Models
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using Abbotware.Core.Configuration;

    /// <summary>
    ///     Configuration class for the SMTP Manger plugin
    /// </summary>
    public class SmtpManagerOptions : BaseOptions, ISmtpManagerOptions
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SmtpManagerOptions" /> class.
        /// </summary>
        /// <param name="host">SMTP server host name</param>
        /// <param name="port">SMTP server port number</param>
        /// <param name="fromAddress">from address used when sending mail</param>
        public SmtpManagerOptions(string host, ushort port, string fromAddress)
            : this(new DnsEndPoint(host, port), fromAddress)
        {
            Arguments.NotNullOrWhitespace(host, nameof(host));
            Arguments.NotNullOrWhitespace(fromAddress, nameof(fromAddress));

            this.LogOptions = true;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SmtpManagerOptions" /> class.
        /// </summary>
        /// <param name="pickupDirectory">pick up directory path</param>
        /// <param name="fromAddress">from address used when sending mail</param>
        public SmtpManagerOptions(Uri pickupDirectory, string fromAddress)
            : this(pickupDirectory, new MailAddress(fromAddress))
        {
            Arguments.NotNull(pickupDirectory, nameof(pickupDirectory));
            Arguments.NotNullOrWhitespace(fromAddress, nameof(fromAddress));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SmtpManagerOptions" /> class.
        /// </summary>
        /// <param name="pickupDirectory">pick up directory path</param>
        /// <param name="from">from address used when sending mail</param>
        public SmtpManagerOptions(Uri pickupDirectory, MailAddress from)
        {
            Arguments.NotNull(pickupDirectory, nameof(pickupDirectory));
            Arguments.NotNull(from, nameof(from));

            this.From = from;
            this.PickupDirectory = pickupDirectory;
            this.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SmtpManagerOptions" /> class.
        /// </summary>
        /// <param name="endpoint">the DNS endpoint for SMTP server</param>
        /// <param name="fromAddress">from address used when sending mail</param>
        public SmtpManagerOptions(DnsEndPoint endpoint, string fromAddress)
            : this(endpoint, new MailAddress(fromAddress))
        {
            Arguments.NotNull(endpoint, nameof(endpoint));
            Arguments.NotNullOrWhitespace(fromAddress, nameof(fromAddress));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SmtpManagerOptions" /> class.
        /// </summary>
        /// <param name="endpoint">the DNS endpoint for SMTP server</param>
        /// <param name="from">from address used when sending mail</param>
        public SmtpManagerOptions(DnsEndPoint endpoint, MailAddress from)
        {
            Arguments.NotNull(endpoint, nameof(endpoint));
            Arguments.NotNull(from, nameof(from));

            this.From = from;
            this.EndPoint = endpoint;
            this.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        /// <inheritdoc/>
        public Encoding MessageEncoding { get; set; } = Encoding.UTF8;

        /// <inheritdoc/>
        public DnsEndPoint? EndPoint { get; set; }

        /// <inheritdoc/>
        public NetworkCredential? Credential { get; set; }

        /// <inheritdoc/>
        public MailAddress From { get; }

        /// <inheritdoc/>
        public bool Ssl { get; set; }

        /// <inheritdoc/>
        public SmtpDeliveryMethod DeliveryMethod { get; }

        /// <inheritdoc/>
        public Uri? PickupDirectory { get; }
    }
}