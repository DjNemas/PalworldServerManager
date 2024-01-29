using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using PalworldServerManagerClient.DI;
using PalworldServerManagerClient.ViewModels;

namespace PalworldServerManagerClient.UserControls.Login
{
    public sealed partial class ServerList : UserControl
    {
        public LoginViewViewModel ViewModel { get; }
        public ServerList()
        {
            InitializeComponent();
            ViewModel = InstanceManager.Instance.GetInstance<LoginViewViewModel>();
        }
    }
}
