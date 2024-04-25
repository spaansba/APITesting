using System.Collections.ObjectModel;
using System.Windows;
using APITesting.Client.WpfClient.Common;
using APITesting.Client.WpfClient.Users.SidePanel;
using APITesting.Contracts;
using CommunityToolkit.Mvvm.Input;

namespace APITesting.Client.WpfClient.Users;

// MIKE: changed to DependencyObject, is this oke?
public sealed partial class AppViewModel : DependencyObject
{
    private readonly ITestClient client;

    public AppViewModel(ITestClient client)
    {
        this.client = client;
        this.createNewPanel = new CreateNewSidePanelViewModel(this);
        this.EditPanel = new EditSidePanelViewModel(this);
        this.SidePanelViewModel = this.EditPanel;
        _ = this.PopulateUsers(); // Start a "fire and forget" task
    }
    
    private readonly CreateNewSidePanelViewModel createNewPanel;
    private readonly EditSidePanelViewModel EditPanel;
    
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
    public async Task AddUser(UserProfileCreateRequest user, CancellationToken cancellationToken)
    {
        this.IsEditing = false;
        SidePanelViewModel = this.createNewPanel;
        // var newUserResponse = await this.client.CreateUser(new ("joe.clark", "Joe Clark", "Joe Clark"), cancellationToken);
        // var newUser = newUserResponse.GetValueOrThrow();
        // Application.Current.Dispatcher.Invoke(() => this.Users.Add(newUser));
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
        this.SidePanelViewModel = this.EditPanel;
      return Task.CompletedTask;
    }

    #region  Dependency Properties
    
    public static readonly DependencyProperty IsEditingProperty = DependencyProperty.Register(
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
    
    private static readonly DependencyProperty SidePanelViewModelProperty = DependencyProperty.Register(
        name: nameof(SidePanelViewModel),
        propertyType: typeof(SidePanelViewModel),
        ownerType: typeof(AppViewModel),
        typeMetadata: new FrameworkPropertyMetadata(null));

    public SidePanelViewModel? SidePanelViewModel
    {
        get => this.GetValue<SidePanelViewModel?>(SidePanelViewModelProperty);
        set => this.SetValue<SidePanelViewModel?>(SidePanelViewModelProperty, value);
    }
    
    #endregion
}