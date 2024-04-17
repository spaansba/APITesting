using APITesting;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add serilog as logger
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders(); //Remove base logger
builder.Logging.AddSerilog(logger);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<TestAPI>();
builder.Services.AddProblemDetails(); // create an exception handler that will generate a ProblemDetails if an exception occurs https://www.rfc-editor.org/rfc/rfc7807.html

builder.Services.Configure<ApplicationOptions>(
    builder.Configuration.GetSection(
        nameof(ApplicationOptions)
    )
);

builder.Services.Configure<ApplicationOptions>(
    builder.Configuration.GetSection(ApplicationOptions.Key));

var app = builder.Build();

// Exception handlers
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/handle-errors?view=aspnetcore-8.0
app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Resource endpoints registering
app.AddTestEndpoints();
app.UseHttpsRedirection();


// TODO: Add to its own file
app.MapGet("options", (
    IOptions<ApplicationOptions> options,
    IOptionsSnapshot<ApplicationOptions> optionsSnapshot,
    IOptionsMonitor<ApplicationOptions> optionsMonitor) =>
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
});

app.Run();

public class ApplicationOptions
{
    public const string Key = "Application";
    public string ExampleValue { get; init; } = string.Empty;
}