using CommunityToolkit.Mvvm.ComponentModel;
using APITesting.Client.WpfClient.Drawer;


namespace APITesting.Client.WpfClient
{
    public sealed partial class AppViewModel : ObservableObject
    {
        [ObservableProperty] 
        private object? content;
        public DrawerHost Drawer { get; } = new DrawerHost();
        public AppViewModel(ITestClient client)
        {
            this.Content = new UserListViewModel(client);
        }
    }
}

