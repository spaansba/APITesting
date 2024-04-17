namespace APITesting;

public class TestAPI
{
    private readonly ILogger<TestAPI> logger;
    private readonly IConfiguration configuration;
    
    public TestAPI(ILogger<TestAPI> logger, IConfiguration configuration)
    {
        this.logger = logger;
        this.configuration = configuration;
    }
    
    public Task<string> GetTest()
    {
        logger.LogInformation("Executing GetTest method");
        logger.LogInformation($"My Custom Setting Value - {configuration["CustomSetting"]}");
        return Task.FromResult("Test");
    }
    public Task<string> PostTest()
    {
        logger.LogCritical("Executing GetPost method");
        return Task.FromResult("Test Post");
    }
}