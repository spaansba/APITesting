using System.ComponentModel.DataAnnotations;
using APITesting.Client.WpfClient.Common;
using APITesting.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace APITesting.Client.WpfClient.Users
{
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
        
        
        // private string? username;
        // public string? Username
        // {
        //     get => this.username;
        //     init
        //     {
        //         if(this.IsNewItem)
        //         {
        //             this.SetProperty(ref this.username, value);
        //         } 
        //     }
        // }

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
    
        [RelayCommand]
        private async Task Save(CancellationToken cancellationToken = default) 
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
  
            WeakReferenceMessenger.Default.Send<CloseDrawerMessage>();
        } 
    }
}