using System.Collections.ObjectModel;
using System.Windows;
using APITesting.Client.WpfClient.Users;
using APITesting.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace APITesting.Client.WpfClient;

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
            var usersToLoad = (await this.client.GetAllUsers()).GetValueOrThrow();
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
    private void AddUser()
    {
        SidePanel = new EditUserViewModel(this.client);
    }

    //MIKE: had to change to "EDIT' because the overload woudnt work in xaml
    [RelayCommand]
    private void EditUser(UserProfileResponse user)
    {
        SidePanel = new EditUserViewModel(this.client, user);
    }

    //MIKe: I removed cancellation token, dont know how to implement
    [RelayCommand]
    private async Task DeleteUser(UserProfileResponse user)
    {
        var response = await this.client.DeleteUser(user.Id);
        await RePopulateUsers();
    }
}

