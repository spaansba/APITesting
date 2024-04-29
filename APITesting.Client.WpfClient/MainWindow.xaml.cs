using System.Windows;
using Dapplo.Microsoft.Extensions.Hosting.Wpf;

namespace APITesting.Client.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IWpfShell
    {
        public MainWindow(AppViewModel appViewModel)
        {
            this.InitializeComponent();
            this.DataContext = appViewModel;
        }
    }
}