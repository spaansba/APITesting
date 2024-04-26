using System.ComponentModel.DataAnnotations;
using APITesting.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;

namespace APITesting.Client.WpfClient.Users;

public partial class EditUserViewModel : ObservableValidator
{
    private readonly ITestClient client;
    public EditUserViewModel(ITestClient client, UserProfileResponse? user = null)
    {
        this.client = client;
        this.Id = user?.Id;
        this.Username = user?.Username;
        this.FullName = user?.FullName;
        this.DisplayName = user?.DisplayName;
    }

    private int? Id { get; } 
    public bool IsNewItem => this.Id is null;

    [ObservableProperty] 
    [NotifyDataErrorInfo] 
    [Required(AllowEmptyStrings = false)] 
    private string? username;

    [ObservableProperty] 
    [NotifyDataErrorInfo] 
    [Required(AllowEmptyStrings = false)] 
    private string? fullName;

    [ObservableProperty] 
    [NotifyDataErrorInfo] 
    [Required(AllowEmptyStrings = false)] 
    private string? displayName;

    
    private async Task Save(CancellationToken cancellationToken) 
    {
        if(this.Id is not null)
        {
            var updateUser = new UserProfileUpdateRequest(FullName, DisplayName);
            await this.client.UpdateUser(this.Id.Value,updateUser, cancellationToken);
        } 
        else
        {
            var createUser = new UserProfileCreateRequest(Username, FullName, DisplayName);
            await this.client.CreateUser(createUser, cancellationToken);
        } 
    } 
}