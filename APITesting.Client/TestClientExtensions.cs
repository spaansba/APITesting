using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace APITesting.Client
{
    public static class TestClientExtensions
    {
        public static IServiceCollection AddTestClient(
            this IServiceCollection services,
            IHostEnvironment hostEnvironment,
            IConfiguration configuration)
    {
        services.AddSingleton<IValidateOptions<TestClientOptions>, ValidateTestClientOptions>()
            .AddOptions<TestClientOptions>()
            .Bind(configuration.GetSection(TestClientOptions.Key))
            .ValidateOnStart();
        
        var httpClientBuilder = services.AddHttpClient(TestClient.HttpClientName, ConfigureHttpClient);
        if (hostEnvironment.IsDevelopment())
        {
            httpClientBuilder.ConfigurePrimaryHttpMessageHandler(static _ => new HttpClientHandler() { ServerCertificateCustomValidationCallback = (_, _, _, _) => true });
        }
        services.AddTransient<ITestClient, TestClient>(CreateTestClient);
        return services;
    }

        private static TestClient CreateTestClient(IServiceProvider services)
    {
        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(TestClient.HttpClientName);
        return new TestClient(httpClient);
    }

        private static void ConfigureHttpClient(IServiceProvider services, HttpClient client)
    {
        var options = services.GetRequiredService<IOptions<TestClientOptions>>();
        client.BaseAddress = new (options.Value.ServerUri);
        // TODO: If needed, add your authorization header
        // client.DefaultRequestHeaders.Authorization =
    }
    }
}