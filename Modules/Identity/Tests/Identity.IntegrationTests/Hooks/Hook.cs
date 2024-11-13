using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;

[Binding]
public class Hook
{
    private const string SERVICE_NAME = "CarPool.Identity.IntegrationTests";
    
    [BeforeScenario]
    public static void BeforeScenario(ScenarioContext scenarioContext)
    {
        // Setup test database or any other test requirements
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        scenarioContext.Add("Configuration", configuration);
    }

    [AfterScenario]
    public static void AfterScenario()
    {
        // Cleanup after each scenario
    }
}