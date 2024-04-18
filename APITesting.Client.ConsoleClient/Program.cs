using APITesting;
using APITesting.Client;

using var httpClient = new HttpClient(); // This is not the correct way of using HttpClient, but this is just temporary...
var client = new TestClient(httpClient);

foreach (var user in await client.GetAllUsers())
{
    Console.WriteLine($"Before Edit: {user}");
    var newUser = await client.UpdateUser(user.Id, new UserProfileUpdateRequest(DisplayName: "New Display Name"));
    Console.WriteLine($"After Edit: {newUser}");
}