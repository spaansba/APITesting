using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using APITesting.Client.WpfClient.Common;
using APITesting.Contracts;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace APITesting.Client.WpfClient.Users.SidePanel;

public sealed class CreateNewSidePanelViewModel : SidePanelViewModel
{
    public override string Header { get; } = "Create New";
    public CreateNewSidePanelViewModel(AppViewModel appViewModel) : base(appViewModel)
    {
        AddNewUser = new RelayCommand(CreateUser);
    }
    
    public ICommand AddNewUser { get; private set; }
    
    private async void CreateUser()
    {
        // Ofcourse here goes a lot more validation
        if (Username is "" or null)
        {
            MessageBox.Show("Fill in a Username");
            return;
        }
        
        if (FullName is "" or null)
        {
            MessageBox.Show("Fill in a Full Name");
            return;
        }

        if (DisplayName is "" or null)
        {
            MessageBox.Show("Fill in a Display Name");
            return;
        }
        
        var createUserProfile = new UserProfileCreateRequest(Username, FullName, DisplayName);
        await this.AppViewModel.AddUser(createUserProfile,default);
    }

    #region Dependecy Properties
    
    private static readonly DependencyProperty UsernameProperty = DependencyProperty.Register(
        name: nameof(Username),
        propertyType: typeof(string),
        ownerType: typeof(CreateNewSidePanelViewModel),
        typeMetadata: new FrameworkPropertyMetadata(string.Empty));
    
    public string? Username
    {
        get => this.GetValue<string?>(UsernameProperty);
        set => this.SetValue<string?>(UsernameProperty, value);
    }
    
    private static readonly DependencyProperty FullNameProperty = DependencyProperty.Register(
        name: nameof(FullName),
        propertyType: typeof(string),
        ownerType: typeof(CreateNewSidePanelViewModel),
        typeMetadata: new FrameworkPropertyMetadata(string.Empty));
    
    public string? FullName
    {
        get => this.GetValue<string?>(FullNameProperty);
        set => this.SetValue<string?>(FullNameProperty, value);
    }
    
    private static readonly DependencyProperty DisplayNameProperty = DependencyProperty.Register(
        name: nameof(DisplayName),
        propertyType: typeof(string),
        ownerType: typeof(CreateNewSidePanelViewModel),
        typeMetadata: new FrameworkPropertyMetadata(string.Empty));
    
    public string? DisplayName
    {
        get => this.GetValue<string?>(DisplayNameProperty);
        set => this.SetValue<string?>(DisplayNameProperty, value);
    }
    
    #endregion
}