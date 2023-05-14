//-----------------------------------------------------------------------
// <copyright file="Scan.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.IntegrationTests.Interop.Amazon
{
    using System;
    using System.Collections.Generic;
    using ProtoBuf;

    /// <summary>
    ///     message class that represents plugin scan
    /// </summary>
    [ProtoContract]
    internal sealed class Scan
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Scan" /> class.
        /// </summary>
        public Scan()
        {
        }

        /// <summary>
        ///     Gets or sets the target entity id
        /// </summary>
        [ProtoMember(5)]
        public Guid TargetEntityId { get; set; }

        /// <summary>
        ///     Gets or sets time the report/plugin ended
        /// </summary>
        [ProtoMember(7)]
        public TimeSpan Duration { get; set; }

        /// <summary>
        ///     Gets or sets sequence #
        /// </summary>
        [ProtoMember(8)]
        public long SequenceId { get; set; }

        /// <summary>
        ///     Gets or sets the report message
        /// </summary>
        [ProtoMember(11)]
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets the exception
        /// </summary>
        [ProtoMember(12)]
        public string Exception { get; set; }

        /// <summary>
        ///     Gets or sets the list of metrics associated with this report
        /// </summary>
        [ProtoMember(13)]
        public List<Metric> Metrics { get; set; } = new List<Metric>();

        /// <summary>
        ///     Gets or sets the message category
        /// </summary>
        [ProtoMember(15)]
        public string Category { get; set; }
    }
}