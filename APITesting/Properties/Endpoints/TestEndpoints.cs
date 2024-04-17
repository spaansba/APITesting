namespace APITesting.Properties.Endpoints;

/// <summary>
/// Extension class to register all Test APIs endpoints
/// </summary>
public static class TestEndpoints
{
    public static void AddTestEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/test", async (TestAPI test) =>
            await test.GetTest());
        
        app.MapPost("/api/test", async (TestAPI test) =>
            await test.PostTest());
    }
}
