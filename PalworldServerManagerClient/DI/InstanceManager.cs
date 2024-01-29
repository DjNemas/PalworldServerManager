using Microsoft.Extensions.DependencyInjection;
using MikuLogger;
using PalworldServerManagerClient.Communication;
using PalworldServerManagerClient.UserControls.Login;
using PalworldServerManagerClient.ViewModels;
using PalworldServerManagerClient.Views;
using PalWorldServerManagerShared.Definitions;
using System.Net;

namespace PalworldServerManagerClient.DI
{
    internal class InstanceManager : IInstanceManager
    {
        public static InstanceManager Instance;

        private ServiceProvider _serviceProvider;

        public InstanceManager()
        {
            Instance = this;
            _serviceProvider = CreateServices();
        }

        public ServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddSingleton<Logger>()
                .AddSingleton<App>()
                .AddSingleton<LoginViewViewModel>()
                .AddTransient<ServerList>()
                .AddTransient<LoginView>()
                .AddTransient<LoginView>()
                .AddTransient<Client>()   
                .BuildServiceProvider();
        }

        public T GetInstance<T>() where T : class => _serviceProvider.GetRequiredService<T>();

        public Client StartClient(IPEndPoint endpoint) => new Client(GetInstance<Logger>(), endpoint);
            
    }
}
