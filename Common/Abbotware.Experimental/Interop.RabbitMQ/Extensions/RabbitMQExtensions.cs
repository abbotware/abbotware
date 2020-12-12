// -----------------------------------------------------------------------
// <copyright file="RabbitMQExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.RabbitMQ.Extensions
{
    using System;
    using System.Globalization;
    using System.Text;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using global::RabbitMQ.Client;

    /// <summary>
    ///     Extensions for RabbitMQ objects
    /// </summary>
    public static class RabbitMQExtensions
    {
        /// <summary>
        ///     Converts IBasicProperties to a string
        /// </summary>
        /// <param name="extendedObject">objected being extended</param>
        /// <returns>formatted string of IBasicProperties</returns>
        public static string ToFormatString(this IBasicProperties extendedObject)
        {
            Arguments.NotNull(extendedObject, nameof(extendedObject));

            var sb = new StringBuilder();

#pragma warning disable CA1062 // Validate arguments of public methods
            if (extendedObject.IsAppIdPresent())
#pragma warning restore CA1062 // Validate arguments of public methods
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "AppID:{0} ", extendedObject.AppId);
            }

            if (extendedObject.IsClusterIdPresent())
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "ClusterId:{0} ", extendedObject.ClusterId);
            }

            if (extendedObject.IsContentEncodingPresent())
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "ContentEncoding:{0} ", extendedObject.ContentEncoding);
            }

            if (extendedObject.IsContentTypePresent())
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "ContentType:{0} ", extendedObject.ContentType);
            }

            if (extendedObject.IsCorrelationIdPresent())
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "CorrelationId:{0} ", extendedObject.CorrelationId);
            }

            if (extendedObject.IsDeliveryModePresent())
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "DeliveryMode:{0} ", extendedObject.DeliveryMode);
            }

            if (extendedObject.IsExpirationPresent())
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "Expiration:{0} ", extendedObject.Expiration);
            }

            if (extendedObject.IsHeadersPresent())
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "Headers:{0} ", extendedObject.Headers.StringFormat());
            }

            if (extendedObject.IsMessageIdPresent())
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "MessageId:{0} ", extendedObject.MessageId);
            }

            if (extendedObject.IsPriorityPresent())
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "Priority:{0} ", extendedObject.Priority);
            }

            if (extendedObject.IsReplyToPresent())
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "ReplyTo:{0} ", extendedObject.ReplyTo);
            }

            if (extendedObject.IsTimestampPresent())
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "Timestamp:{0} ", extendedObject.Timestamp);
            }

            if (extendedObject.IsTypePresent())
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "Type:{0} ", extendedObject.Type);
            }

            if (extendedObject.IsUserIdPresent())
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "UserId:{0} ", extendedObject.UserId);
            }

            return sb.ToString();
        }
    }
}