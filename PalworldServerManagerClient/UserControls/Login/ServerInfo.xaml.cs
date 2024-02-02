using Microsoft.UI.Xaml.Controls;
using PalworldServerManagerClient.ViewModels;
using PalworldServerManagerClient.DI;
using Microsoft.UI.Xaml;
using System;
using PalworldServerManagerClient.Pages;
using PalworldServerManagerClient.Communication;
using PalWorldServerManagerShared.Model;
using System.Net;
using MikuLogger;
using System.Linq;
using PalWorldServerManagerShared.Definitions;
using PalworldServerManagerClient.Definitions;
using PalworldServerManagerClient.Helper;
using System.Threading.Tasks;
using System.Text.Json;

namespace PalworldServerManagerClient.UserControls.Login
{
    public sealed partial class ServerInfo : UserControl, IShowPopup
    {
        public LoginViewViewModel ViewModel { get; set; }
        private Logger _logger = new Logger();
        public ServerInfo()
        {
            InitializeComponent();
            ViewModel = InstanceManager.Instance.GetInstance<LoginViewViewModel>();
            _logger = InstanceManager.Instance.GetInstance<Logger>();
        }

        public void DisconnectAsync(object sender, RoutedEventArgs e)
        {
            foreach (var item in Client.ConnectedTcpClients)
            {
                item.CloseConnection();
            }
        }

        public async void ConnectAsync(object sender, RoutedEventArgs e)
        {
            Client readonlyClient = null;
            Client client = null;

            try
            {
                if (IPEndPoint.TryParse(tb_ServerIP.Text, out var ip))
                {
                    readonlyClient = new Client(_logger);
                    await readonlyClient.ConnectToServerAsync(ip);

                    client = new Client(_logger);
                    await client.ConnectToServerAsync(ip);
                }

                var domain = Dns.GetHostAddresses(tb_ServerIP.Text);
                if (domain.Count() == 1)
                {
                    var newIP = new IPEndPoint(domain.ElementAt(0), Convert.ToInt32(tb_ServerPort.Text));

                    readonlyClient = new Client(_logger);
                    await readonlyClient.ConnectToServerAsync(newIP, true);

                    client = new Client(_logger);
                    await client.ConnectToServerAsync(newIP);
                }

                if (readonlyClient.IsConnected)
                {
                    Client.ConnectedTcpClients.Add(readonlyClient);
                    var jsonString = JsonSerializer.Serialize(new Connection() { ClientType = 1 });
                    var message = new Message() { Command = Command.Connect, JsonData = jsonString };
                    await readonlyClient.SendDataAsync(message);
                }
                if (client.IsConnected)
                {
                    Client.ConnectedTcpClients.Add(client);
                    var jsonString = JsonSerializer.Serialize(new Connection() { ClientType = 2 });
                    var message2 = new Message() { Command = Command.Connect, JsonData = jsonString };
                    await client.SendDataAsync(message2);
                }

                if (!readonlyClient.IsConnected || !client.IsConnected)
                {
                    if (readonlyClient.IsConnected)
                        readonlyClient.CloseConnection();
                    if (client.IsConnected)
                        client.CloseConnection();

                    ShowErrorMessage("Connection Error", "Connection to Server Failed. Please check your connection data and if your server is running.");
                    return;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Meow:" + ex);
            }
        }

        public async void ShowErrorMessage(string title, string message)
            => await ErrorPopupHelper.ShowErrorPopup(XamlRoot, title, message);
    }
}
