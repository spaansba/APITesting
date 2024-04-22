using APITesting.Client;
using APITesting.Client.ConsoleClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


// IMPORTANT: This is not the correct way of using HttpClient, but this is just temporary...
using var clientHandler = new HttpClientHandler();
// Ignore any certificate warnings (DEVELOPMENT ONLY!!!!)
clientHandler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
using var httpClient = new HttpClient(clientHandler);
httpClient.BaseAddress = new Uri("https://localhost:7072"); 
// TODO: If your API requires authentication
// httpClient.DefaultRequestHeaders.Authorization = // Add your stuff here

var newHostBuilder = Host.CreateApplicationBuilder();
var services = newHostBuilder.Services;

services.AddOptions<TestClientOptions>()
        .Bind(newHostBuilder.Configuration.GetSection(TestClientOptions.Key))
        .ValidateOnStart(); // Check all validation attributes, if there is a problem throw an error (URL) (Required) etc
   //     .ValidateDataAnnotations() // If we want to use the Microsoft.Extensions.Options.DataAnnotations nuget package with validation attributes
services.AddTestClient(newHostBuilder.Environment, newHostBuilder.Configuration)
        .AddHostedService<ConsoleClientService>();

// RunConsoleAsync will run all Hosted Services ExecuteAsync methods one by one
await newHostBuilder.RunConsoleAsync();
