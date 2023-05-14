//-----------------------------------------------------------------------
// <copyright file="Metric.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.IntegrationTests.Interop.Amazon
{
    using ProtoBuf;

    /// <summary>
    /// report metric classs
    /// </summary>
    [ProtoContract]
    internal sealed class Metric
    {
        /// <summary>
        /// Gets or sets the metric type
        /// </summary>
        [ProtoMember(1)]
        public MetricTypeId Id { get; set; }

        /// <summary>
        /// Gets or sets the metric Group
        /// </summary>
        [ProtoMember(2)]
        public string Group { get; set; }

        /// <summary>
        /// Gets or sets the metric key
        /// </summary>
        [ProtoMember(3)]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the string value
        /// </summary>
        [ProtoMember(4)]
        public string StringValue { get; set; }

        /// <summary>
        /// Gets or sets the binary value
        /// </summary>
        [ProtoMember(5)]
        public byte[] Binary { get; set; }

        /// <summary>
        /// Gets or sets the string value
        /// </summary>
        [ProtoMember(6)]
        public double? DoubleValue { get; set; }

        /// <summary>
        /// Gets or sets the string value
        /// </summary>
        [ProtoMember(7)]
        public long? LongValue { get; set; }
    }
}