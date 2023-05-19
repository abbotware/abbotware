// -----------------------------------------------------------------------
// <copyright file="QualityOfService.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Messaging.Amqp.Configuration
{
    using System;
    using Abbotware.Core;

    /// <summary>
    /// Configuration class for Quality of Service settings
    /// </summary>
    public class QualityOfService
    {
        /// <summary>
        ///     internal set once property for pre fetch count
        /// </summary>
        private readonly SetOnceProperty<ushort> preFetchCount = new("PreFetchCount");

        /// <summary>
        ///     internal set once property for pre fetch global
        /// </summary>
        private readonly SetOnceProperty<bool> preFetchGlobal = new("PreFetchGlobal");

        /// <summary>
        ///     internal set once property for pre fetch size
        /// </summary>
        private readonly SetOnceProperty<uint> preFetchSize = new("PreFetchSize");

        /// <summary>
        ///     Gets or sets the pre fetch size (maximum amount of content (measured in octets) that the server will deliver, 0 if
        ///     unlimited)
        /// </summary>
        public uint PreFetchSize
        {
            get
            {
                if (this.preFetchSize.HasValue)
                {
                    return this.preFetchSize.Value;
                }

                return 0;
            }

            set
            {
                Arguments.NotNull(value, nameof(this.PreFetchSize), "PreFetchSize set value requires non-null");

                this.preFetchSize.Value = value;
            }
        }

        /// <summary>
        ///     Gets or sets the pre fetch count (maximum number of messages that the server will deliver, 0 if unlimited)
        /// </summary>
        public ushort PreFetchCount
        {
            get
            {
                if (this.preFetchCount.HasValue)
                {
                    return this.preFetchCount.Value;
                }

                return 0;
            }

            set
            {
                Arguments.NotNull(value, nameof(this.PreFetchCount), "PreFetchCount set value requires non-null");

                this.preFetchCount.Value = value;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether or not the pre-fetch values are global(Connection) or channel
        /// </summary>
        /// <remarks>
        ///     By default the 'quality of service' settings apply to the current channel only. If this field is set, they are
        ///     applied to the entire connection.
        /// </remarks>
        public bool PreFetchGlobal
        {
            get
            {
                if (this.preFetchGlobal.HasValue)
                {
                    return this.preFetchGlobal.Value;
                }

                return false;
            }

            set
            {
                Arguments.NotNull(value, nameof(this.PreFetchGlobal), "PreFetchGlobal set value requires non-null");

                this.preFetchGlobal.Value = value;
            }
        }
    }
}