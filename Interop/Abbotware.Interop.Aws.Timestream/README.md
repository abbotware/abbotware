# ﻿Abbotware.Interop.Aws.Timestream

C# Fluent and Attribute based publishers for AWS Timestream that supports single and multi-value based off POCOs

## Attributes

Attributes can be are used to define dimensions, measures, and time fields.

```c#
[MeasureName("Data")]
public class Poco
{
    [Dimension]
    public string PropertyA { get; set; }

    [Dimension]
    public string PropertyB { get; set; }

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


### Poco Publisher Example 

```c#
// create write options for target database / table
var options = new TimestreamOptions() { Database = "database", Table = "table" };
            
// create publisher with options + logger
using var c = new PocoPublisher<MultiMeasureTest>(options, NullLogger.Instance);

// create message
var poco = new Poco() { ... }

// publish
var p = await c.PublishAsync(poco, ct);
```


## Fluent API

Fluent API require a little more setup, but can be used on objects you have no control over

```c#
public class Poco
{
    public string PropertyA { get; set; }
    public string PropertyB { get; set; }
    public int? ValueA { get; set; }
    public string ValueB { get; set; }
    public decimal? ValueC { get; set; }
    public bool? ValueD { get; set; }
    public DateTimeOffset? Time { get; set; }
}
```

### Define Protocol via Protocol Builder
```c#
// supply name for multi measure name
var pb = new ProtocolBuilder<Poco>("metrics");
    pb.AddDimension(x => x.PropertyA);
    pb.AddDimension(x => x.PropertyB);
    pb.AddMeasure(x => x.ValueA);
    pb.AddMeasure(x => x.ValueB);
    pb.AddMeasure(x => x.ValueC);
    pb.AddMeasure(x => x.ValueD);
    pb.AddTime(x => x.Time, TimeUnitType.Milliseconds);

var protocol = pb.Build();
```

### Create Publisher

```c#      
// create write options for target database / table
var options = new TimestreamOptions() { Database = "database", Table = "table" };

// create publisher with options + protocol + logger
using var p = new TimestreamPublisher<SingleMeasureTest>(options, protocol, NullLogger.Instance);

// create message
var poco = new Poco() { ... }

// publish
var p = await c.PublishAsync(poco, ct);
```


## Settings File

```c#
var options = ConfigurationHelper.AppSettingsJson("settings.json").BindSection<TimestreamOptions>(TimestreamOptions.DefaultSection);
```
