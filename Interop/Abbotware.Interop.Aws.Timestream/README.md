# ﻿Abbotware.Interop.Aws.Timestream

POCO based publisher for AWS Timestream that supports single and multi-value measures

## POCO Attributes

attributes are used to define dimensions, measures, and time fields

```c#
[MeasureName("Data")]
public class Poco
{
    [Dimension]
    public string PropertyA { get; set; }

    [Dimension]
    public string PropertyAB { get; set; }

    [MeasureValue]
    public int? ValueA { get; set; }

    [MeasureValue]
    public string ValueB { get; set; }

    [MeasureValue]
    public decimal? ValueC { get; set; }

    [MeasureValue]
    public bool? ValueD { get; set; }

    [Time]
    public DateTimeOffset? Time { get; set; }
}
```


## Publisher 

```c#
// 1. create
var options = mew TimestreamOptions( ... );
            
// 2. create
using var c = new TimestreamPublisher<MultiMeasureTest>(options, ... );

// 3. publish

var poco = new Poco() { ... }
var p = await c.PublishAsync(poco, ct);
```


## Settings File

```c#
var options = ConfigurationHelper.AppSettingsJson("settings.json").BindSection<TimestreamOptions>(TimestreamOptions.DefaultSection);
```
