using Microsoft.Extensions.DependencyInjection;
using MikuLogger;
using PalworldServerManagerServer.Communication;
using PalworldServerManagerServer.Installation;
using PalWorldServerManagerShared.Definitions;

namespace PalworldServerManagerServer.DI
{
    internal class InstanceManager : IInstanceManager
    {
        private ServiceProvider _serviceProvider;

        public InstanceManager()
        {
            _serviceProvider = CreateServices();
        }

        public ServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddSingleton<Logger>()
                .AddSingleton<ClientListener>()
                .AddTransient<SteamCmd>()
                .BuildServiceProvider();
        }

        public ClientListener StartClientListener(int port) => new ClientListener(GetInstance<Logger>(), port);

        public T GetInstance<T>() where T : class => _serviceProvider.GetRequiredService<T>();
    }
}
