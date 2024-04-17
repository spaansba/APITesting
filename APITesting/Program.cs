using APITesting;
using APITesting.Properties.Endpoints;
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

builder.Services.Configure<ApplicationOptions>(
    builder.Configuration.GetSection(nameof(ApplicationOptions)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Resource endpoints registering
app.AddTestEndpoints();
app.UseHttpsRedirection();

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
    public string ExampleValue { get; init; } = string.Empty;
}