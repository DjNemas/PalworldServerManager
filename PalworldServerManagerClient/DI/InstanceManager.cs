using Microsoft.Extensions.DependencyInjection;
using MikuLogger;
using PalworldServerManagerClient.Communication;
using PalWorldServerManagerShared.Definitions;
using System.Net;

namespace PalworldServerManagerClient.DI
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
                .AddTransient<Logger>()
                .AddTransient<Client>()
                .BuildServiceProvider();
        }

        public T GetInstance<T>() where T : class
        => _serviceProvider.GetRequiredService<T>();

        public Client StartClient(IPEndPoint endpoint)
        {
            return new Client(_serviceProvider.GetRequiredService<Logger>(), endpoint);
        }
            
    }
}
