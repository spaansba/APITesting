using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace APITesting.Client.ConsoleClient;

public sealed class ConsoleClientService : BackgroundService
{
    private readonly IServiceProvider serviceProvider;
    private readonly IHostApplicationLifetime hostLifetime;

    public ConsoleClientService(IServiceProvider serviceProvider, IHostApplicationLifetime hostLifetime)
    {
        this.serviceProvider = serviceProvider;
        this.hostLifetime = hostLifetime;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var client = this.serviceProvider.GetRequiredService<ITestClient>(); // Ask for the DI'ted client
        
        // Because first await is locked behind user input we use await before so the host can keep building
        await Task.Yield();
        
        var newTestUser = new UserProfileCreateRequest(Username: "j.smith", FullName: "John Smith", DisplayName: "John");

        if ((await client.CreateUser(newTestUser, stoppingToken)).TryGetValue(out var testUser, out var error))
        {
            Console.WriteLine("Temp user added");
            Console.WriteLine(testUser.ToString());
        }
        else
        {
            Console.WriteLine("ERROR");
            Console.WriteLine(error.ToString());
        }

        var stopOperation = false;
        do
        {
            Console.WriteLine("[G]et, [P]ost, P[a]tch, [D]elete, Press [X] to cancel");
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.G:
                    var user = await APIMethods.GetUser(client, stoppingToken); 
                    Console.WriteLine(user?.ToString());
                    break;
                case ConsoleKey.P:
                    await APIMethods.CreateUser(client, stoppingToken);
                    break;
                case ConsoleKey.A:
                    await APIMethods.PatchUser(client, stoppingToken);
                    break;
                case ConsoleKey.D:
                    await APIMethods.DeleteUser(client, stoppingToken);
                    break;
                case ConsoleKey.X:
                    Console.WriteLine("Operation cancelled");
                    this.hostLifetime.StopApplication(); // Cancel the Host (and thus all IHostedServices)
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
            Console.WriteLine("");
        } while (!stopOperation);

    }
}