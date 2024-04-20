using System.Net;

namespace APITesting.Client.ConsoleClient;

// I know these should be in separate files but its mostly for testing
public static class APIMethods
{
    private static int GetIntFromUser()
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
    
    private static string GetStringFromUser()
    {
        string userInput;
        do
        {
            userInput = Console.ReadLine() ?? string.Empty;
        } while (string.IsNullOrEmpty(userInput)); // Check if the input is null or empty

        return userInput;
    }
    private static bool IdExists(TestClient client, int id)
    {
        if (client.GetUser(id).Result.TryGetValue(out _, out var error))
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

    private static int GetValidIdFromUser(TestClient client)
    {
        int id;
        bool isValidId;
        do
        {
            Console.WriteLine("Enter an ID (integer)");
            id = GetIntFromUser();
            isValidId = IdExists(client, id);
            if (!isValidId)
            {
                Console.WriteLine("Press x to cancel Get request, Press any other key to try another id");
                if (Console.ReadKey(true).Key == ConsoleKey.X)
                    return -1;
            }
        } while (isValidId);

        return id;
    }
    
    public static UserProfileResponse? GetUser(TestClient client)
    {

        var id = GetValidIdFromUser(client);
        if (id == -1)
            return null;
        
        if (!client.GetUser(id).Result.TryGetValue(out var user, out var error))
        {
            Console.WriteLine("ERROR");
            Console.WriteLine(error.ToString());
            return null;
        }
        return user;
    }
    
    public static void CreateUser(TestClient client)
    {
        Console.WriteLine("Enter a username");
        var userName = GetStringFromUser();
        Console.WriteLine("Enter a first name");
        var firstName = GetStringFromUser();
        Console.WriteLine("Enter a DisplayName");
        var displayName = GetStringFromUser();
        var newUser = new UserProfileCreateRequest(userName, firstName, displayName);
        
        if(client.CreateUser(newUser).Result.TryGetValue(out var user, out var error))
        {
            Console.WriteLine($"User {displayName} successfully created with ID x");
        }
        else
        {
            Console.WriteLine("ERROR");
            Console.WriteLine(error.ToString());
        }
    }

    public static void PatchUser(TestClient client)
    {
        
    }
}