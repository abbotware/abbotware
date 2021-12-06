// -----------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Reviewed AA>", Scope = "member", Target = "~M:Abbotware.Core.Helpers.NetworkHelper.LocalNonLoopbackIPV4~System.Net.IPAddress[]")]
[assembly: SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "<Reviewed AA>", Scope = "member", Target = "~P:Abbotware.Core.Threading.Counters.ActiveInstanceCounter`1.GlobalActiveCount")]
[assembly: SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "<Reviewed AA>", Scope = "member", Target = "~P:Abbotware.Core.Threading.Counters.TypeCreatedCounter`1.Count")]
[assembly: SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "<Reviewed AA>", Scope = "type", Target = "~T:Abbotware.Core.Cache.ICacheableSortedSet`2")]
[assembly: SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "<Reviewed AA>>", Scope = "member", Target = "~M:Abbotware.Core.Objects.IFactory`1.Destroy(`0)")]
[assembly: SuppressMessage("Security", "CA5351:Do Not Use Broken Cryptographic Algorithms", Justification = "<Reviewed AA>", Scope = "member", Target = "~M:Abbotware.Core.Security.MD5Helper.ToGuid(System.Byte[])~System.Guid")]
[assembly: SuppressMessage("Security", "CA5351:Do Not Use Broken Cryptographic Algorithms", Justification = "<Reviewed AA>", Scope = "member", Target = "~M:Abbotware.Core.Security.MD5Helper.GenerateHash(System.String,Abbotware.Core.Security.HashStringFormat)~System.String")]
[assembly: SuppressMessage("Security", "CA2109:Review visible event handlers", Justification = "<Reviewed AA>", Scope = "member", Target = "~M:Abbotware.Core.Messaging.Integration.Base.BaseMqConsumer.OnDelivery(System.Object,Abbotware.Core.Messaging.Integration.DeliveryEventArgs)")]
[assembly: SuppressMessage("Security", "CA2300:Do not use insecure deserializer BinaryFormatter", Justification = "Uses a TypeBinder", Scope = "member", Target = "~M:Abbotware.Core.Serialization.Helpers.BinaryFormatterHelper.DeserializeViaBinaryFormatter(System.Byte[],System.Type)~System.Object")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File should have header", Justification = "<Reviewed AA>", Scope = "namespace", Target = "~N:System.Runtime.CompilerServices")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Reviewed AA>", Scope = "member", Target = "~M:Abbotware.Core.Net.Configuration.Models.ServicePointManagerOptions.CreateDefault~Abbotware.Core.Net.Configuration.Models.ServicePointManagerOptions")]
