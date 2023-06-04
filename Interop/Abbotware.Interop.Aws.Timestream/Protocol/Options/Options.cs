namespace Abbotware.Interop.Aws.Timestream.Protocol.Options
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Amazon.TimestreamWrite;

    public record class Option<TMessage, TValue, TString>(TValue Type, Func<TMessage, TString> Lookup);

    public record class DimensionValueOptions<TMessage>(DimensionValueType Type, Func<TMessage, string> Lookup) : Option<TMessage, DimensionValueType, string>(Type, Lookup);

    public record class MeasureValueOptions<TMessage>(MeasureValueType Type, Func<TMessage, string?> Lookup) : Option<TMessage, MeasureValueType, string?>(Type, Lookup);

    public record class TimeValueOptions<TMessage>(TimeUnitType Type, Func<TMessage, DateTimeOffset> Lookup);
}
