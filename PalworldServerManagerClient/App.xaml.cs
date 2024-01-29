using Microsoft.UI.Xaml;
using PalworldServerManagerClient.DI;
using PalworldServerManagerClient.Views;
using System.Threading.Tasks;

namespace PalworldServerManagerClient
{
    public partial class App : Application
    {
        private readonly InstanceManager _instanceManager;
        public App()
        {
            _instanceManager = new InstanceManager();
            _instanceManager.CreateServices();
            InitializeComponent();
        }
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            m_window = _instanceManager.GetInstance<LoginView>();
            m_window.Activate();
        }

        private Window m_window;
    }
}
