// -----------------------------------------------------------------------
// <copyright file="EnvelopeBuilder.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.RabbitMQ.Plugins
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Abbotware.Core;
    using Abbotware.Core.Messaging.Amqp.Configuration;
    using Abbotware.Core.Messaging.Configuration;
    using Abbotware.Core.Messaging.Configuration.Models;
    using Abbotware.Core.Messaging.Integration.Configuration;
    using Abbotware.Core.Messaging.Integration.Configuration.Models;
    using global::RabbitMQ.Client;

    /// <summary>
    /// Enveloper builder methods
    /// </summary>
    public static class EnvelopeBuilder
    {
        /// <summary>
        /// Creates a message envelope from a basic get result
        /// </summary>
        /// <param name="result">rabbitmq basic get result</param>
        /// <returns>message envelope</returns>
        public static MessageEnvelope Create(BasicGetResult result)
        {
            result = Arguments.EnsureNotNull(result, nameof(result));

            var e = Create(result.DeliveryTag.ToString(CultureInfo.InvariantCulture), result.Redelivered, result.Exchange, result.RoutingKey, result.Body);

            e.DeliveryProperties.MessageCount = result.MessageCount;

            UpdateEnvelope(e, result.BasicProperties);

            return e;
        }

        /// <summary>
        /// Creates a message envelope
        /// </summary>
        /// <param name="deliveryTag">delivery tag</param>
        /// <param name="redelivered">redelivered flag</param>
        /// <param name="exchange">exchange name</param>
        /// <param name="routingKey">routing key / topic</param>
        /// <param name="properties">rabbitmq IBasicProperties</param>
        /// <param name="body">message content</param>
        /// <param name="consumerTag">consumer tag</param>
        /// <returns>message envelope</returns>
        public static MessageEnvelope Create(string deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body, string consumerTag)
        {
            properties = Arguments.EnsureNotNull(properties, nameof(properties));

            var e = Create(deliveryTag, redelivered, exchange, routingKey, body);

            e.DeliveryProperties.ConsumerTag = consumerTag;

            UpdateEnvelope(e, properties);

            return e;
        }

        /// <summary>
        /// Updates rabbitmq IBasicProperties with valuse from a message envelope
        /// </summary>
        /// <param name="target">target IBasicProperties</param>
        /// <param name="source">source message envelope</param>
        public static void UpdateBasicProperties(IBasicProperties target, IMessageEnvelope source)
        {
            target = Arguments.EnsureNotNull(target, nameof(target));
            source = Arguments.EnsureNotNull(source, nameof(source));

            if (source.PublishProperties.Persistent.HasValue)
            {
                if (source.PublishProperties.Persistent.Value)
                {
                    target.DeliveryMode = (byte)DeliveryMode.Persistent;
                }
                else
                {
                    target.DeliveryMode = (byte)DeliveryMode.Transient;
                }
            }

            if (!string.IsNullOrWhiteSpace(source.CommonHeaders.AppId))
            {
                target.AppId = source.CommonHeaders.AppId;
            }

            if (!string.IsNullOrWhiteSpace(source.CommonHeaders.ClusterId))
            {
                target.ClusterId = source.CommonHeaders.ClusterId;
            }

            if (!string.IsNullOrWhiteSpace(source.CommonHeaders.ContentEncoding))
            {
                target.ContentEncoding = source.CommonHeaders.ContentEncoding;
            }

            if (!string.IsNullOrWhiteSpace(source.CommonHeaders.ContentType))
            {
                target.ContentType = source.CommonHeaders.ContentType;
            }

            if (!string.IsNullOrWhiteSpace(source.CommonHeaders.CorrelationId))
            {
                target.CorrelationId = source.CommonHeaders.CorrelationId;
            }

            if (source.CommonHeaders.Expiration.HasValue)
            {
                target.Expiration = source.CommonHeaders.Expiration.Value.TotalMilliseconds.ToString(CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(source.CommonHeaders.MessageId))
            {
                target.MessageId = source.CommonHeaders.MessageId;
            }

            if (source.CommonHeaders.Priority.HasValue)
            {
                target.Priority = source.CommonHeaders.Priority.Value;
            }

            if (!string.IsNullOrWhiteSpace(source.CommonHeaders.ReplyTo))
            {
                target.ReplyTo = source.CommonHeaders.ReplyTo;
            }

            if (source.CommonHeaders.Timestamp.HasValue)
            {
                target.Timestamp = new AmqpTimestamp(source.CommonHeaders.Timestamp.Value.ToUnixTimeSeconds());
            }

            if (source.CommonHeaders.Expiration.HasValue)
            {
                target.Expiration = source.CommonHeaders.Expiration.Value.TotalMilliseconds.ToString(CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(source.CommonHeaders.MessageType))
            {
                target.Type = source.CommonHeaders.MessageType;
            }

            if (!string.IsNullOrWhiteSpace(source.CommonHeaders.UserId))
            {
                target.UserId = source.CommonHeaders.UserId;
            }

            if (source.Headers.Any())
            {
                target.Headers = source.Headers.ToDictionary(x => x.Key, x => x.Value);
            }
        }

        private static MessageEnvelope Create(string deliveryTag, bool redelivered, string exchange, string routingKey, ReadOnlyMemory<byte> body)
        {
            var e = new MessageEnvelope
            {
                Body = body,
            };

            e.PublishProperties.Exchange = exchange;
            e.PublishProperties.RoutingKey = routingKey;
            e.DeliveryProperties.DeliveryTag = deliveryTag;
            e.DeliveryProperties.Redelivered = redelivered;

            return e;
        }

        private static void UpdateEnvelope(MessageEnvelope target, IBasicProperties source)
        {
            target.CommonHeaders.AppId = source.AppId;
            target.CommonHeaders.ClusterId = source.ClusterId;
            target.CommonHeaders.ContentEncoding = source.ContentEncoding;
            target.CommonHeaders.ContentType = source.ContentType;
            target.CommonHeaders.CorrelationId = source.CorrelationId;

            if (source.IsExpirationPresent())
            {
                if (TimeSpan.TryParse(source.Expiration, out var expiration))
                {
                    target.CommonHeaders.Expiration = expiration;
                }
            }

            target.CommonHeaders.MessageId = source.MessageId;

            if (source.IsPriorityPresent())
            {
                target.CommonHeaders.Priority = source.Priority;
            }

            target.PublishProperties.Persistent = source.Persistent;
            target.CommonHeaders.ReplyTo = source.ReplyTo;

            if (source.IsTimestampPresent())
            {
                target.CommonHeaders.Timestamp = DateTimeOffset.FromUnixTimeSeconds(source.Timestamp.UnixTime);
            }

            target.CommonHeaders.MessageType = source.Type;

            target.Headers.Clear();

            var kv = source.Headers.ToDictionary(x => x.Key, x => x.Value).ToList();

            foreach (var k in kv)
            {
                target.Headers.Add(k);
            }
        }
    }
}
