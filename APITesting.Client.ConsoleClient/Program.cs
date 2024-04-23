using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace APITesting.Client.ConsoleClient;

internal static class Program
{
    private static async Task Main(string[] args)
    {
   
        var newHostBuilder = Host.CreateApplicationBuilder(args);
        var services = newHostBuilder.Services;

        // If we want to use the Microsoft.Extensions.Options.DataAnnotations nuget package with validation attributes
        // services.AddOptions<TestClientOptions>()
        //         .Bind(newHostBuilder.Configuration.GetSection(TestClientOptions.Key))
        //         .ValidateOnStart() // Check all validation attributes, if there is a problem throw an error (URL) (Required) etc
        //         .ValidateDataAnnotations(); 

        services.AddTestClient(newHostBuilder.Environment, newHostBuilder.Configuration)
            .AddHostedService<ConsoleClientService>();

        // RunConsoleAsync will run all Hosted Services ExecuteAsync methods one by one
        await newHostBuilder.RunConsoleAsync();
    }
}