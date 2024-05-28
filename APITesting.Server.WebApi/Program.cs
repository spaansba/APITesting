using APITesting.Contracts;
using APITesting.EndPoints;
using Microsoft.Extensions.Options;
using Serilog;

namespace APITesting
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            if(args is ["migrate"])
            {
                RunMigrations();
                return;
            }
            
            ConfigureDapper();
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
            builder.Services
                .AddProblemDetails(); // create an exception handler that will generate a ProblemDetails if an exception occurs https://www.rfc-editor.org/rfc/rfc7807.html

            builder.Services.Configure<ApplicationOptions>(builder.Configuration.GetSection(ApplicationOptions.Key));
            builder.Services.AddSingleton<IDatabase, Database>();
            builder.Services.AddOptions<DatabaseOptions>()
                .Bind(builder.Configuration.GetSection(DatabaseOptions.ConfigurationKey))
                .ValidateOnStart();

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
            
            await app.RunAsync();
        }
 
        private static void RunMigrations()
        {
            // We create a mini version of program.cs otherwise we cant access the connectionString which is configured by 
            // the configuration system which is only available in the middle of creating the server

            var builder = Host.CreateApplicationBuilder();

            builder.Services
                .AddOptions<DatabaseOptions>()
                .Bind(builder.Configuration.GetSection(DatabaseOptions.ConfigurationKey));

            var host = builder.Build();
            var options = host.Services.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            var connectionString = options.CreateConnectionString();
            var success = Server.Migrations.Program.RunMigrations(connectionString, out var exception);

            Console.WriteLine($"Migrations {( success? "successful" : "failed" )}");
            Console.WriteLine(exception);
        }

        private static void ConfigureDapper()
        {
            // allow names like User_Id be allowed to match properties/fields like UserId 
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }
    }
}

