# Abbotware
Collection of many utility classes, interfaces, extension methods that have been accumulated over the years. 

[![Build](https://github.com/abbotware/abbotware/actions/workflows/main.yml/badge.svg)](https://github.com/abbotware/abbotware/actions/workflows/main.yml)

* Designed to be DI friendly and container agnostic
* .NetStandard 2.0 and 2.1 compatible
* uses nullable checks via C# 8.0
* Code Coverage via ([Coverlet](https://github.com/coverlet-coverage/coverlet))
* Coverage Report aggregation via ([ReportGenerator](https://github.com/danielpalme/ReportGenerator))

### Abbotware.Core
This assembly has no major dependencies except for **Microsoft.Extensions.Logging.Abstractions**.  The internal components all log to the Microsoft.Extensions.Logging.ILogger interface.  For NetStandard2.0 /and NetStandard2.1, additional dependencies have been added only to add in missing features that have been added to later version of .Net.

### Abbotware.Interop.{Library} 
These assemblies contain interop/wrapper classes around various third-party libraries to encapsulate initialization and logic.  Sometimes the are abstracted away as via interfaces  

### Abbotware.{Feature}
These assemblies contain feature/modules that group a common area. Sometimes these are biased towards a specific implementation and may link to specific Interop assemblies creating an external dependency.  
Example.  `Abbotware.Data` is heavily biased towards SQL Server and Entity Framework and depends on `Abbotware.Interop.EntityFramework`



### Abbotware.{Feature}.Using.{Container} 
These assemblies contain fluent api / builder syntax to help a **feature** be registered wuth a specific **container**. 
Example: `Abbotware.Data.Using.Castle` provides many ways to register contexts in castle container.

# Base Objects

the following base objects make up the majority of components in this library to provide common and consistent features and behavior

|Class| Inherits | Description |
|---|---|---|
|`BaseLoggable`| n/a | minimal class with a logger |
|`BaseComponent`| `BaseLoggable` |  adds in disposable, eager and lazy initialization |
|`BaseComponent<TConfiguration>`|`BaseComponent` |  adds in configuration  |


# Interface and Plugins

Many features are implemented as plugins that implement an interface.  

An example is: `IBinarySerializaton` - there are plugins such as `DataContractSerializer` and `XmlSerializer`

General interfaces are defined in `Abbotware.Core` and plugins maybe be in secondary assemblies to allow consumption of only the minimal number of thirdparty nugets in the end project.


# Specific Libraries



| Library                              | Description                                                  |
| ------------------------------------ | ------------------------------------------------------------ |
| **Abbotware.ShellCommand**           | A wrapper library for executing shell commands. Captures standard output/error with timestamps as well as auto-kills processed after a specified timeout |
| **Abbotware.Interop.Iso**            | Enum classes and metadata for the free ISO Standard Codes ([see more](https://github.com/abbotware/abbotware/tree/main/Interop/Abbotware.Interop.Iso#abbotwareinteropiso)) |
| **Abbotware.Interop.Aws.Timestream** | Fluent and POCO based publishers for AWS Timestream ([see more](https://github.com/abbotware/abbotware/tree/main/Interop/Abbotware.Interop.Aws.Timestream#abbotwareinteropawstimestream)) |





