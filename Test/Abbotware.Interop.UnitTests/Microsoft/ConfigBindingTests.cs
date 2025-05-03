namespace Abbotware.UnitTests.Interop.Microsoft;

using System.Collections.Generic;
using global::Microsoft.Extensions.Configuration;
using NUnit.Framework;

[TestFixture]
public class ConfigBindingTests
{
    [Test]

    public void BindsRecordFromJsonCorrectly()
    {
        // Arrange
        var inMemorySettings = new Dictionary<string, string?>
        {
            { "MyConfig:Name", "Moria" },
            { "MyConfig:Threshold", "42" },
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        // Act
        var myConfig = configuration.GetSection("MyConfig")
            .Get<MyConfig>()!;

        // Assert
        Assert.That(myConfig, Is.Not.Null);
        Assert.That(myConfig.Name, Is.EqualTo("Moria"));
        Assert.That(myConfig.Threshold, Is.EqualTo(42));
    }

    public record MyConfig(string Name, int Threshold);
}