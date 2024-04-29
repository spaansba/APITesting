using APITesting.Client.WpfClient.Users;
using Dapplo.Microsoft.Extensions.Hosting.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace APITesting.Client.WpfClient
{
    internal static class Program
    {
        [STAThread]
        private static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .ConfigureServices()
                .ConfigureServices(static services => services.AddHostedService<DataInitializationService>()) // is adding an IHostedService that needs to start before the WPF hosted service.eds to
                .AddWpfToHostBuilder() // This should always be the last thing we do before building
                .Build()
                .RunAsync();
        }

        private static IHostBuilder AddWpfToHostBuilder(this IHostBuilder hostBuilder)
            => hostBuilder.ConfigureWpf(ConfigureWpfBuilder)
                .UseWpfLifetime();

        private static void ConfigureWpfBuilder(IWpfBuilder wpfBuilder) => wpfBuilder
            .UseApplication<App>()
            .UseWindow<MainWindow>();

        private static IHostBuilder ConfigureServices(this IHostBuilder hostBuilder)
            => hostBuilder.ConfigureServices(ConfigureServices);

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
            => services.AddTestClient(context.HostingEnvironment, context.Configuration)
                .AddTransient<AppViewModel>();
    }
}