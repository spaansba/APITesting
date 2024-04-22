using System.Net;
using System.Runtime.InteropServices.JavaScript;
using APITesting;
using APITesting.Client;
using APITesting.Client.ConsoleClient;
using APITesting.Client.Result;

// IMPORTANT: This is not the correct way of using HttpClient, but this is just temporary...
using var clientHandler = new HttpClientHandler();
// Ignore any certificate warnings (DEVELOPMENT ONLY!!!!)
clientHandler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
using var httpClient = new HttpClient(clientHandler);
httpClient.BaseAddress = new Uri("https://localhost:7072"); 
// TODO: If your API requires authentication
// httpClient.DefaultRequestHeaders.Authorization = // Add your stuff here

var client = new TestClient(httpClient);

var newTestUser = new UserProfileCreateRequest(Username: "j.smith", FullName: "John Smith", DisplayName: "John");

if ((await client.CreateUser(newTestUser)).TryGetValue(out var testUser, out var error))
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
var cancellationToken = new CancellationToken();
do
{
    Console.WriteLine("[G]et, [P]ost, P[a]tch, [D]elete, Press [X] to cancel");
    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.G:
            var user = await APIMethods.GetUser(client, cancellationToken); 
            Console.WriteLine(user?.ToString());
            break;
        case ConsoleKey.P:
            await APIMethods.CreateUser(client, cancellationToken);
            break;
        case ConsoleKey.A:
            await APIMethods.PatchUser(client, cancellationToken);
            break;
        case ConsoleKey.D:
            await APIMethods.DeleteUser(client, cancellationToken);
            break;
        case ConsoleKey.X:
            Console.WriteLine("Operation cancelled");
            stopOperation = true;
            break;
        default:
            Console.WriteLine("Invalid input. Please try again.");
            break;
    }
    Console.WriteLine("");
} while (!stopOperation);




