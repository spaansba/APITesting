using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace APITesting.Client.WpfClient.Data;

public sealed class DataManager : BackgroundService
{
    private readonly IServiceProvider serviceProvider;
    private ITestClient? client;
    private ITestClient Client => this.client ??= this.serviceProvider.GetRequiredService<ITestClient>();

    public DataManager(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    private static ITestClient GetClient(DataManager dataManager) => dataManager.Client;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        this.Register();
        return Task.CompletedTask;
    }
    
    private void Register()
    {
        RequestUsersMessage.Register(this, GetClient, ITestClient.GetAllUsers);
        RequestUserMessage.Register(this, GetClient, ITestClient.GetUser);
    }

}