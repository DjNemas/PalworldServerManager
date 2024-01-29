using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using PalworldServerManagerClient.ViewModels;
using PalworldServerManagerClient.DI;

namespace PalworldServerManagerClient.UserControls.Login
{
    public sealed partial class ServerInformation : UserControl
    {
        public LoginViewViewModel ViewModel { get; set; }
        public ServerInformation()
        {
            InitializeComponent();
            ViewModel = InstanceManager.Instance.GetInstance<LoginViewViewModel>();
        }

        private void cb_UseAuthentication_Checked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            grid_Password.Visibility = Visibility.Visible;
        }

        private void cb_UseAuthentication_Unchecked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            grid_Password.Visibility = Visibility.Collapsed;
        }
    }
}
