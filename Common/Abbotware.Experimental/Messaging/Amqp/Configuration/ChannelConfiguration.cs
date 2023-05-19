// -----------------------------------------------------------------------
// <copyright file="ChannelConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Messaging.Amqp.Configuration
{
    using Abbotware.Core;

    /// <summary>
    ///     Configuration class for ChannelManager
    /// </summary>
    public class ChannelConfiguration
    {
        /// <summary>
        ///     internal set once property for channel mode
        /// </summary>
        private readonly SetOnceProperty<ChannelMode> channelMode = new("ChannelMode");

        /// <summary>
        ///     internal set once property for use quality of service
        /// </summary>
        private readonly SetOnceProperty<QualityOfService> qualityOfService = new("QualityOfService");

        /// <summary>
        ///     Gets or sets the channel mode
        /// </summary>
        public ChannelMode? Mode
        {
            get
            {
                if (this.channelMode.HasValue)
                {
                    return this.channelMode.Value;
                }

                return null;
            }

            set
            {
                this.channelMode.Value = value!.Value;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether not to use quality of service
        /// </summary>
        public QualityOfService? QualityOfService
        {
            get
            {
                if (this.qualityOfService.HasValue)
                {
                    return this.qualityOfService.Value!;
                }

                return null;
            }

            set
            {
                this.qualityOfService.Value = value;
            }
        }
    }
}