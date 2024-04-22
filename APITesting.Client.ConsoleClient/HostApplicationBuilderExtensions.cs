using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace APITesting.Client.ConsoleClient;

public static class HostApplicationBuilderExtensions
{
    public static async Task RunConsoleAsync(this HostApplicationBuilder hostBuilder, CancellationToken cancellationToken = default)
    {
        hostBuilder.Services.AddSingleton<IHostLifetime, ConsoleLifetime>();
        var host = hostBuilder.Build(); //After this you cant add items to the IServiceCollection
        
        await host.RunAsync(cancellationToken);
    }
}