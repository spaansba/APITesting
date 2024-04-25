using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;

namespace APITesting.Client.WpfClient.Users.SidePanel;

public abstract class SidePanelViewModel : DependencyObject
{
    public AppViewModel AppViewModel { get; set; }
    public abstract string Header { get; }
    protected SidePanelViewModel(AppViewModel appViewModel)
    {
        AppViewModel = appViewModel;
    }
}