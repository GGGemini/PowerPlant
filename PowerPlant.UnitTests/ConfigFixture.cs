using Microsoft.Extensions.Configuration;

namespace PowerPlant.UnitTests;

public class ConfigFixture
{
    public IConfiguration Config { get; }
    
    public ConfigFixture()
    {
        Config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Development.json")
            .Build();
    }
}