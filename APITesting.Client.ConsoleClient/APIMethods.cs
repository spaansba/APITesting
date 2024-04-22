using System.Net;

namespace APITesting.Client.ConsoleClient;

// I know these should be in separate files but its mostly for testing
public static class APIMethods
{
    public static async Task<UserProfileResponse?> GetUser(TestClient client)
    {

        var id = await ApiHelperMethods.GetValidIdFromUser(client);
        if (id == -1)
            return null;

        if ((await client.GetUser(id)).TryGetValue(out var user, out var error))
        {
            return user;
        }

        Console.WriteLine("ERROR");
        Console.WriteLine(error.ToString());
        return null;
    }
    
    public static async Task CreateUser(TestClient client)
    {
        Console.WriteLine("Enter a username");
        var userName = ApiHelperMethods.GetStringFromUser();
        Console.WriteLine("Enter a first name");
        var firstName = ApiHelperMethods.GetStringFromUser();
        Console.WriteLine("Enter a DisplayName");
        var displayName = ApiHelperMethods.GetStringFromUser();
        var newUser = new UserProfileCreateRequest(userName, firstName, displayName);
        
        if(( await client.CreateUser(newUser)).TryGetValue(out var user, out var error))
        {
            Console.WriteLine($"User {displayName} successfully created with ID {user.Id}");
        }
        else
        {
            Console.WriteLine("ERROR");
            Console.WriteLine(error.ToString());
        }
    }

    public static async Task PatchUser(TestClient client)
    {
        var id = await ApiHelperMethods.GetValidIdFromUser(client);
        if (id == -1)
            return;
        
        var result = await client.GetUser(id);
        if (!result.TryGetValue(out var currentUserProfile, out var getError))
        {
            Console.WriteLine("ERROR");
            Console.WriteLine(getError.ToString());
            return;
        }
        
        var newFullName = currentUserProfile.FullName;
        var newDisplayName = currentUserProfile.DisplayName;
        
        bool stopAltering = false;
        do
        {
            Console.WriteLine("Which attribute do you want to alter?");
            Console.WriteLine("[F]ullname, [D]isplayname");
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.F:
                    Console.WriteLine($"Enter a new fullname, current firstname: {currentUserProfile.FullName}");
                    newFullName = ApiHelperMethods.GetStringFromUser();
                    stopAltering = ApiHelperMethods.AskToStopOperation("Press X to cancel operation, any other key to keep altering this ID");
                    break;
                case ConsoleKey.D:
                    Console.WriteLine($"Enter a new display name, current display name: {currentUserProfile.DisplayName}");
                    newDisplayName = ApiHelperMethods.GetStringFromUser();
                    stopAltering = ApiHelperMethods.AskToStopOperation("Press X to cancel operation, any other key to keep altering this ID");
                    break;
                default:
                    Console.WriteLine("Invalid input, please try again");
                    break;
            }
        } while (!stopAltering);
        
        var updatedUser = new UserProfileUpdateRequest(newFullName, newDisplayName);

        if ((await client.UpdateUser(id, updatedUser)).TryGetValue(out var user, out var updateError))
        {
            Console.WriteLine($"User {user.DisplayName} successfully updated");
        }
        else
        {
            Console.WriteLine("ERROR");
            Console.WriteLine(updateError.ToString());
        }
        
    }

    public static async Task DeleteUser(TestClient client)
    {
        var id = await ApiHelperMethods.GetValidIdFromUser(client);
        if (id == -1)
            return;
        
        if (client.DeleteUser(id).IsCompletedSuccessfully)
        {
            Console.WriteLine($"User with id {id} successfully deleted");
        }
        else
        {
            Console.WriteLine("ERROR");
        }
    }
}