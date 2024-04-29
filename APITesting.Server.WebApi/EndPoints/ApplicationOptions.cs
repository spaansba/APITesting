using Microsoft.Extensions.Options;

namespace APITesting.EndPoints
{
    public class ApplicationOptions
    {
        // Key for the GetSection, so we can later change the name of this class without any problems
        public const string Key = "ApplicationOptions";
        public string ExampleValue { get; init; } = string.Empty;

        public IResult GetApplicationOptions(IOptions<ApplicationOptions> options,
            IOptionsSnapshot<ApplicationOptions> optionsSnapshot,
            IOptionsMonitor<ApplicationOptions> optionsMonitor)
    {
        var response = new
        {
            // Singleton, does not change when the appsetting ApplicationOptions examplevalue changes
            OptionsValue = options.Value.ExampleValue,
    
            // Does change when the appsetting ApplicationOptions examplevalue changes
            SnapshotValue = optionsSnapshot.Value.ExampleValue,
    
            // Is singleton but still shows latets appsetting ApplicationOptions examplevalue value
            MonitorValue = optionsMonitor.CurrentValue.ExampleValue
        };
        return Results.Ok((response));
    }
    }

    public static class ApplicationOptionsEndPoints
    {
        public static void AddApplicationOptionsEndPoints(this IEndpointRouteBuilder app)
    {
       // app.MapGet("options", GetApplicationOptions);
    }
    
        // private static async Task<IResult> GetApplicationOptions(
        //     [FromServices] ApplicationOptions options
        // )
        // {
        //     var result = await options.GetApplicationOptions();
        //     return TypedResults.Ok(result);
        // }
    
    }
}

