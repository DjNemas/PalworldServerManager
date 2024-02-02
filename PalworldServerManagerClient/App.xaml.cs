using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using PalworldServerManagerClient.Database;
using PalworldServerManagerClient.DI;
using PalworldServerManagerClient.FolderManagment;
using PalworldServerManagerClient.Views;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PalworldServerManagerClient
{
    public partial class App : Application
    {
#if DEBUG
        [DllImport("kernel32.dll")]
        public static extern int AllocConsole();
#endif
        private readonly InstanceManager _instanceManager;
        public App()
        {
#if DEBUG
            AllocConsole();
#endif
            _instanceManager = new InstanceManager();
            _instanceManager.CreateServices();
            _instanceManager.GetInstance<DatabaseContext>().Database.Migrate();
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
