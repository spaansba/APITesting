using APITesting.EndPoints;
using Serilog;

namespace APITesting
{
    internal static class Program
    {
        private static void Main(string[] args)
    {
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
        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<ApplicationOptions>();
        builder.Services.AddProblemDetails(); // create an exception handler that will generate a ProblemDetails if an exception occurs https://www.rfc-editor.org/rfc/rfc7807.html

        builder.Services.Configure<ApplicationOptions>(builder.Configuration.GetSection(ApplicationOptions.Key));

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
        app.AddUserEndpoints();
        app.UseHttpsRedirection();

        //
        // app.MapGet("options", (IOptions<ApplicationOptions> options,
        //     IOptionsSnapshot<ApplicationOptions> optionsSnapshot,
        //     IOptionsMonitor<ApplicationOptions> optionsMonitor) => 
        //     options.Value.GetResult(options, optionsSnapshot, optionsMonitor));

        app.Run();
    }
    }
}