using Microsoft.Extensions.DependencyInjection;
using MikuLogger;
using PalworldServerManagerClient.Communication;
using PalworldServerManagerClient.DI;
using System.Net;
using System.Text;

namespace PalworldServerManagerServer
{
    internal class Program
    {
        private static InstanceManager? _instanceManager;
        private static Client? _client;
        static void Main(string[] args) => 
            MainAsync(args).GetAwaiter().GetResult();


        private static async Task MainAsync(string[] args)
        {
            _instanceManager = new InstanceManager();
            _instanceManager.CreateServices();

            var logger = _instanceManager.GetInstance<Logger>();
            logger.LogInfo("Client Application");

            string domain = "djnemashome.de";
            IPAddress? ipAddress;
            if (!IPAddress.TryParse(domain, out ipAddress))
                ipAddress = Dns.GetHostEntry(domain).AddressList[0];

            _client = _instanceManager.StartClient(new IPEndPoint(ipAddress, 3939));
            _client.ConnectToServer();

            ReadConsole();

            Task.Delay(-1).Wait();
        }

        private static async void ReadConsole()
        {
            while (true)
            {
                string? userText = await Console.In.ReadLineAsync();
                if(userText == "1")
                {
                    var builder = new StringBuilder();
                    for (int i = 0; i < 1024 / 2; i++)
                    {
                        builder.Append("M");
                    }
                    userText = builder.ToString();  
                    _client?.SendData(userText);
                }
                if(userText is not null)
                    _client?.SendData(userText);
            }

        }
    }
}
