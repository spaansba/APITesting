using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace APITesting.Client.WpfClient;

public sealed class DataInitializationService : IHostedService
{
    private readonly IServiceProvider serviceProvider;

    public DataInitializationService(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var client = this.serviceProvider.GetRequiredService<ITestClient>();
        await client.CreateUser(new("jacob.smith", "Jacob Smith", "Jake Smith"), cancellationToken);
        await client.CreateUser(new("margaret.smith", "Margaret Smith", "Maggie Smith"), cancellationToken);
    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}