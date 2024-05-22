using APITesting.Contracts;
using APITesting.EndPoints;
using Serilog;

namespace APITesting
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
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
            builder.Services.AddHostedService<DatabaseTest>();

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
            
            var UserRepository = new UserRepository();
            var newUser = await UserRepository.AddSingleUser(new UserProfileCreateRequest("Bas", "Bassie", "Bassie111"));
            Console.WriteLine($"Added User {newUser}");

        }
        
        private static void ConfigureDapper()
        {
            // allow names like User_Id be allowed to match properties/fields like UserId 
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }
    }
}

