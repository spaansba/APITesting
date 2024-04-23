using Dapplo.Microsoft.Extensions.Hosting.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace APITesting.Client.WpfClient;

internal static class Program
{
    [STAThread]
    private static async Task Main(string[] args)
    {
        await Host.CreateDefaultBuilder(args)
            .ConfigureServices()
            .ConfigureWpf()
            .UseWpfLifetime()
            .Build()
            .RunAsync();
    }

    private static IHostBuilder ConfigureWpf(this IHostBuilder hostBuilder)
        => hostBuilder.ConfigureWpf(ConfigureWpf);

    private static void ConfigureWpf(IWpfBuilder wpfBuilder) => wpfBuilder
        .UseApplication<App>()
        .UseWindow<MainWindow>();

    private static IHostBuilder ConfigureServices(this IHostBuilder hostBuilder)
        => hostBuilder.ConfigureServices(ConfigureServices);

    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        => services.AddTestClient(context.HostingEnvironment, context.Configuration);
}