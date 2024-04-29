using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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