using MikuLogger;
using PalWorldServerManagerShared.Extensions;
using PalWorldServerManagerShared.Model;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace PalworldServerManagerServer.Communication
{
    internal class ClientListener
    {
        public static TcpClient? ReadonlyClient { get; private set; }
        private readonly Logger _logger;
        private readonly TcpListener _listener;

        public ClientListener(Logger logger, int listenOnPort) 
        {
            _logger = logger;
            _listener = new TcpListener(IPAddress.Any, listenOnPort);
        }        

        public void Start()
        {
            _logger.LogInfo("Start TCP Listener");
            _listener.Start();
            WaitForClients();
        }

        private void WaitForClients()
        {
            _listener.BeginAcceptTcpClient(IncomingClient, null);
            _logger.LogInfo("Waiting for Clients...");
        }

        private void IncomingClient(IAsyncResult result)
        {
            TcpClient client = _listener.EndAcceptTcpClient(result);
            _logger.LogInfo("Client Connected. Sending Welcome Message");

            _ = ReadData(client);

            WaitForClients();
        }

        public async Task SendData(TcpClient client, Message message)
        {
            if (!client.Connected)
                return;

            var jsonString = message.ToJsonString();

            if (jsonString.Count() == 1024)
                jsonString += " ";

            try
            {
                var byteToSend = Encoding.UTF8.GetBytes(jsonString);
                await client.GetStream().WriteAsync(byteToSend);
            }
            catch (Exception)
            {
                _logger.LogInfo("Client Lost Connection");
                client.Close();
                return;
            }
        }

        private async Task ReadData(TcpClient client)
        { 
            var stringBuilder = new StringBuilder();

            while (true)
            {
                var networkStream = client.GetStream();
                if (!client.Connected)
                    return;

                var buffer = new byte[1_024];
                int bytesToRead = 0;
                try
                {
                    bytesToRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Client Lost Connection");
                    _logger.LogError(ex);
                    client.Close();
                    return;
                }
                var stringData = Encoding.UTF8.GetString(buffer, 0, bytesToRead);
                stringBuilder.Append(stringData);

                if (bytesToRead < buffer.Length)
                {                    
                    var stringMessage = stringBuilder.ToString();
#if DEBUG
                    _logger.LogInfo("Message Recieved: " + stringMessage);
                    _logger.LogDebug(client.Connected);
#endif              
                    stringBuilder.Clear();

                    if (!string.IsNullOrEmpty(stringMessage))
                    {
                        var message = JsonSerializer.Deserialize<Message>(stringMessage);
                        if (message is not null)
                            IncomingDataHandler.HandleWelcomeClients(client, message);
                    }
                }
            }
        }

        public static void SetReadonlyClient(TcpClient client) => ReadonlyClient = client;
    }
}
