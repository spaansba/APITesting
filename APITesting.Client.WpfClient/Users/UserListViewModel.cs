using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using APITesting.Client.WpfClient.Common;
using APITesting.Client.WpfClient.Data;
using APITesting.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace APITesting.Client.WpfClient.Users;

public sealed partial class UserListViewModel : ObservableObject
{
    private readonly ITestClient client;

    public UserListViewModel(ITestClient client)
    {
        this.client = client;
        _ = this.PopulateUsers(); // Start a "fire and forget" task
        
    
    }

    public ObservableCollection<UserProfileResponse> Users { get; } = new();
    
    [ObservableProperty] private object? sidePanel;

    private async Task PopulateUsers()
    {
        try
        {
            //UserListViewModel sends a requestUserMessage, then DataManager listens to that message and runs client. GetAllUsers which in turn returns a Task<IEnumerable<UserProfileResponse>>
            // which will be sent back to UserListViewModel
            var usersToLoad = await RequestUsersMessage.SendOrThrow();
            Application.Current.Dispatcher.Invoke(() =>
                {
                    foreach (var user in usersToLoad)
                        this.Users.Add(user);
                }
            );
        }
        catch (Exception e)
        {
            // TODO: Make a better exception handling process?
            MessageBox.Show(e.ToString());
        }
    }

    // MIKE: Not the best approach since I am deleting it all and then populating again (might be slow for larger sets of data)
    private async Task RePopulateUsers()
    {
        Application.Current.Dispatcher.Invoke(() => Users.Clear());
        await this.PopulateUsers();
    }

    [RelayCommand]
    private async Task AddUser()
    {
        var drawerContent = new EditUserViewModel(this.client);

        var drawerMessage = new OpenDrawerMessage(drawerContent);

        await WeakReferenceMessenger.Default.Send(drawerMessage);
        await RePopulateUsers();
        ; // <-- Set a breakpoint here. 

    }
    
    [RelayCommand]
    private async Task EditUser(UserProfileResponse user)
    {
        var drawerContent = new EditUserViewModel(this.client, user);
        var drawerMessage = new OpenDrawerMessage(drawerContent);
        await WeakReferenceMessenger.Default.Send(drawerMessage);
        await RePopulateUsers();
    }

    //MIKE: I removed cancellation token, dont know how to implement
    [RelayCommand]
    private async Task DeleteUser(UserProfileResponse user)
    {
        var response = await this.client.DeleteUser(user.Id);
        await RePopulateUsers();
    }
}
