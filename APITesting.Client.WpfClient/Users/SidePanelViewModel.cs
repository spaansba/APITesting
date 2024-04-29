using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;

namespace APITesting.Client.WpfClient.Users.SidePanel
{
    public abstract class SidePanelViewModel : ObservableValidator
    {
        public WpfClient.AppViewModel AppViewModel { get; set; }
        protected SidePanelViewModel(WpfClient.AppViewModel appViewModel)
        {
            AppViewModel = appViewModel;
        }
    }
}