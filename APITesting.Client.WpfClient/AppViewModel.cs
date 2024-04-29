using CommunityToolkit.Mvvm.ComponentModel;
using APITesting.Client.WpfClient.Common;

namespace APITesting.Client.WpfClient
{
    public sealed partial class AppViewModel : ObservableObject
    {
        [ObservableProperty] 
        private object? content;
        public Drawer Drawer { get; } = new();
        public AppViewModel(ITestClient client)
        {
            this.Content = new Users.UserListViewModel(client);
        }
    }
}

