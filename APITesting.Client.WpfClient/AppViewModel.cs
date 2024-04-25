using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using APITesting.Client.WpfClient.Common;

namespace APITesting.Client.WpfClient;

// MIKE: changed to DependencyObject, is this oke?
public sealed partial class AppViewModel : DependencyObject
{
    private readonly ITestClient client;

    public AppViewModel(ITestClient client)
    {
        this.client = client;
        _ = this.PopulateUsers(); // Start a "fire and forget" task
    }

    
    
    private async Task PopulateUsers()
    {
        try
        {
            var usersToLoad = (await this.client.GetAllUsers()).GetValueOrThrow();
            Application.Current.Dispatcher.Invoke(() =>
                {
                    foreach(var user in usersToLoad)
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

    private async Task RePopulateUsers()
    {
        Application.Current.Dispatcher.Invoke(() => Users.Clear());
        await this.PopulateUsers();
    }
    
    public ObservableCollection<UserProfileResponse> Users { get; } = new();

    [RelayCommand]
    private async Task AddUser(CancellationToken cancellationToken)
    {
        var newUserResponse = await this.client.CreateUser(new ("joe.clark", "Joe Clark", "Joe Clark"), cancellationToken);
        var newUser = newUserResponse.GetValueOrThrow();
        Application.Current.Dispatcher.Invoke(() => this.Users.Add(newUser));
    }
    
    [RelayCommand]
    private async Task DeleteUser(UserProfileResponse user, CancellationToken cancellationToken)
    {
        var response = await this.client.DeleteUser(user.Id, cancellationToken);
        await RePopulateUsers();
    }
    
    [RelayCommand]
    private async Task FinalizeEditUser(UserProfileResponse user, CancellationToken cancellationToken)
    {
        IsEditing = false;
        var newUser = new UserProfileUpdateRequest(user.FullName, user.DisplayName);

        if (SelectedUser is null)
            throw new NotSupportedException();
        
        if ((await client.UpdateUser(SelectedUser.Id, newUser, cancellationToken)).TryGetValue(out _, out var updateError))
        {
            await RePopulateUsers();
            SelectedUser = default;
            MessageBox.Show($"{user.Username} updates succesfully");
        }
        else
        {
            Console.WriteLine("ERROR");
            Console.WriteLine(updateError.ToString());
            MessageBox.Show(updateError.ToString());
        }
    }
    
    [RelayCommand]
    private Task EditUser(UserProfileResponse user)
    {
        IsEditing = true;
        SelectedUser = user;
      //  throw new NotImplementedException();
      return Task.CompletedTask;
    }

    private static readonly DependencyProperty IsEditingProperty = DependencyProperty.Register(
        name: nameof(IsEditing),
        propertyType: typeof(bool),
        ownerType: typeof(AppViewModel),
        typeMetadata: new FrameworkPropertyMetadata(Boxes.False));
    
    public bool IsEditing
    {
        get => this.GetValue<bool>(IsEditingProperty);
        set
        {
            this.SetValue<bool>(IsEditingProperty, value);
            DeletePossible = !value;
        }
    }
    
    private static readonly DependencyProperty DeletePossibleProperty = DependencyProperty.Register(
        name: nameof(DeletePossible),
        propertyType: typeof(bool),
        ownerType: typeof(AppViewModel),
        typeMetadata: new FrameworkPropertyMetadata(Boxes.True));
    
    public bool DeletePossible
    {
        get => this.GetValue<bool>(DeletePossibleProperty);
        set => this.SetValue<bool>(DeletePossibleProperty, value);
    }
    
    private static readonly DependencyProperty SelectedUserProperty = DependencyProperty.Register(
        name: nameof(SelectedUser),
        propertyType: typeof(UserProfileResponse),
        ownerType: typeof(AppViewModel),
        typeMetadata: new FrameworkPropertyMetadata(null));
    
    public UserProfileResponse? SelectedUser
    {
        get => this.GetValue<UserProfileResponse?>(SelectedUserProperty);
        set => this.SetValue<UserProfileResponse?>(SelectedUserProperty, value);
    }

    public string emptyString = string.Empty;
}