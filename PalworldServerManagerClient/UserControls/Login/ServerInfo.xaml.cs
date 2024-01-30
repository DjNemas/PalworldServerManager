using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using PalworldServerManagerClient.ViewModels;
using PalworldServerManagerClient.DI;

namespace PalworldServerManagerClient.UserControls.Login
{
    public sealed partial class ServerInfo : UserControl
    {
        public LoginViewViewModel ViewModel { get; set; }
        public ServerInfo()
        {
            InitializeComponent();
            ViewModel = InstanceManager.Instance.GetInstance<LoginViewViewModel>();
        }
    }
}
