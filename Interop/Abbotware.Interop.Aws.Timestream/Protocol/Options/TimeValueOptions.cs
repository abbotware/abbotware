// -----------------------------------------------------------------------
// <copyright file="TimeValueOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Options
{
    using System;
    using System.Globalization;
    using Abbotware.Interop.Aws.Timestream.Protocol.Internal;
    using Amazon.TimestreamWrite;
    using Amazon.TimestreamWrite.Model;

    /// <summary>
    /// TimeValue Options class
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    /// <typeparam name="TProperty">property type</typeparam>
    /// <param name="Type">TimeUnitType</param>
    /// <param name="Expression">property evaluator expression</param>
    /// <param name="Converter">converter function</param>
    /// <param name="SourceName">source property name</param>
    public record class TimeValueOptions<TMessage, TProperty>(TimeUnitType Type, Func<TMessage, TProperty> Expression, Func<TProperty, DateTimeOffset> Converter, string SourceName) : MessagePropertyOption<TimeUnitType, TMessage, TProperty, DateTimeOffset>(Type, Expression, Converter, SourceName, "Time"), IRecordUpdater<TMessage>
        where TMessage : notnull
    {
        /// <inheritdoc/>>
        public void Update(TMessage message, Record record)
        {
            var t = this.Expression(message);
            var dto = this.Converter(t);

            switch (this.Type)
            {
                case TimeUnitType.Milliseconds:
                    var unix = dto.ToUnixTimeMilliseconds();

                    if (unix < 0)
                    {
                        throw new ArgumentOutOfRangeException($"{unix} is not a valild Unix Time in milliseconds. {this.SourceName}:{t}");
                    }

                    record.Time = unix.ToString(CultureInfo.InvariantCulture);
                    record.TimeUnit = TimeUnit.MILLISECONDS;

                    break;
                default:
                    throw new NotSupportedException($"TimeUnitType:{this.Type} currently unsupported");
            }
        }
    }
}
