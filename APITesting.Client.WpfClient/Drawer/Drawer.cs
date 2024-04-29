using CommunityToolkit.Mvvm.ComponentModel;

namespace APITesting.Client.WpfClient.Drawer;

public partial class DrawerHost : ObservableObject
{
    [ObservableProperty] 
    private object? content;
    [ObservableProperty] 
    private bool isOpen;
}