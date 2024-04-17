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
    
    public Task<string> GetUserInformation()
    {
        logger.LogInformation("Executing GetTest method");
        logger.LogInformation($"My Custom Setting Value - {configuration["CustomSetting"]}");
        return Task.FromResult("Test");
    }
    public Task<string> UpdateUserProfile()
    {
        logger.LogCritical("Executing GetPost method");
        return Task.FromResult("Test Post");
    }
}

/// <summary>
/// Extension class to register all Test APIs endpoints
/// </summary>


public static class TestEndpoints
{
    public static void AddTestEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/test", GetTest);

        app.MapPost("/api/test", PostTest);
    }
    
    private static async Task<IResult> GetTest(TestAPI test) 
    {
        var result = await test.GetUserInformation();
        return TypedResults.Ok(result);
    }
    
    private static async Task<IResult> PostTest(TestAPI test) 
    {
        var result = await test.UpdateUserProfile();
        return TypedResults.Ok(result);
    }
}
