using System.Net;

namespace APITesting.Client.ConsoleClient;

/// <summary>
/// Class designed to check if the user selected ID exists
/// </summary>
public static class ApiHelperMethods
{
    public static bool AskToStopOperation(string message)
    {
        Console.WriteLine(message);
        if (Console.ReadKey(true).Key == ConsoleKey.X)
        {
            Console.WriteLine("Operation cancelled");
            return true;
        }
        return false;
    }
    
    public static int GetIntFromUser()
    {
        int selectedId;
        bool isValidInput;
        do
        {
            var userInput = Console.ReadLine();

            isValidInput = int.TryParse(userInput, out selectedId);

            if (!isValidInput)
            {
                Console.WriteLine("Invalid input. Please enter an integer.");
            }
        } while (!isValidInput);
        return selectedId;
    }
    
    public static string GetStringFromUser()
    {
        string userInput;
        do
        {
            userInput = Console.ReadLine() ?? string.Empty;
        } while (string.IsNullOrEmpty(userInput));

        return userInput;
    }

    public static async ValueTask<int> GetValidIdFromUser(TestClient client, CancellationToken cancellationToken)
    {
        int id;
        bool isValidId;
        do
        {
            Console.WriteLine("Enter an ID (integer)");
            id = GetIntFromUser();
            isValidId = await IdExists(client, id, cancellationToken);
            if (!isValidId)
            {
                if (AskToStopOperation("Press x to cancel Get request, Press any other key to try another id"))
                    return -1;
            }
        } while (!isValidId);

        return id;
    }
    
    private static async ValueTask<bool> IdExists(TestClient client, int id, CancellationToken cancellationToken)
    {
        if ((await client.GetUser(id, cancellationToken)).TryGetValue(out _, out var error))
        {
            return true;
        }
        
        if (error.StatusCode == HttpStatusCode.NotFound)
        {
            Console.WriteLine("404 - User Not Found");
        }
        else
        {
            Console.WriteLine("ERROR");
            Console.WriteLine(error.ToString());
        }
        return false;
    }


}