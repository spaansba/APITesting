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

var newUser = new UserProfileCreateRequest(Username: "j.smith", FullName: "John Smith", DisplayName: "John");

string? userInput;
do
{
    Console.WriteLine("Get, Post, Patch, Put, Delete, Press x to cancel");
    userInput = Console.ReadLine()?.ToLower();
    switch (userInput)
    {
        case "get":
            var user = APIMethods.GetUser(client); 
            Console.WriteLine(user?.ToString());
            break;
        case "post":
            Console.WriteLine("Create a new user");
            break;
        case "patch":
            
            break;
        case "put":
            APIMethods.CreateUser(client);
            break;
        case "delete":
            Console.WriteLine("ID of user to be deleted");
            break;
        case "x":
            Console.WriteLine("Operation cancelled");
            break;
        default:
            Console.WriteLine("Invalid input. Please try again.");
            break;
    }
} while (userInput != "x");



// if ((await client.CreateUser(newUser)).TryGetValue(out var user, out var error))
// {
//     Console.WriteLine("SUCCESS");
//     Console.WriteLine(user.ToString());
// }
// else
// {
//     Console.WriteLine("ERROR");
//     Console.WriteLine(error.ToString());
// }

