# abbotware
Collection of many utility classes, interfaces, extension methods that have been accumulated over the years. 

* Designed to be DI friendly and contiainer agnostic
* .NetStandard 2.0 and 2.1 compatible
* uses nullable checks via C# 8.0
* Code Coverage via Coverlet
* Coverage Report aggreagation via ReportGenerator

### Abbotware.Core
This assembly has no major dependencies on any other nugets. Starting with Net5, there will be a dependency on **Microsoft.Extensions.Logging.Abstractions** to use the Microsoft.Extensions.Logging.ILogger interface.  All internal logging will be routed via an adapter (and eventually replaced completely)

### Abbotware.Interop.{Library} 
These assemblies contain interop/wrapper classes around various thirdparty libraries to encapsulate initialization and logic.  Somtimes the are abstracted away as via interfaces  

### Abbotware.{Feature}
These assemblies contain feature/modules that group a common area. Sometimes these are biased towards a specific implementation and may link to specific Interop assemblies creating an external dependency.  
Example.  `Abbotware.Data` is heavily biased towards SQL Server and Entity Framework and depends on `Abbotware.Interop.EntityFramework`



### Abbotware.{Feature}.Using.{Container} 
These assemblies contain fluent api / builder syntax to help a **feature** be registered wuth a specific **container**. 
Example: `Abbotware.Data.Using.Castle` provides many ways to register contexts in castle container.

# Base Objects

the following base objects make up the majority of components in this library to provide common and consistent features and behavior

|class| inherits | description |
|---|---|---|
|`BaseLoggable`| n/a | minimal class with a logger | 
|`BaseComponent`| `BaseLoggable` |  adds in disposable, eager and lazy initialization |
|`BaseComponent<TConfiguration>`|`BaseComponent` |  adds in configuration  |


# Interface and Plugins

Many features are implemented as plugins that implement an interface.  

An example is: `IBinarySerializaton` - there are plugins such as `DataContractSerializer` and `XmlSerializer`

General interfaces are defined in `Abbotware.Core` and plugins maybe be in secondary assemblies to allow consumption of only the minimal number of thirdparty nugets in the end project.


# Specific Libraries

Abbotware.ShellCommand - A wrapper library for executing shell commands. Captures standard output/error with timestamsp as well as auto-kills processed after a specified timeout.
