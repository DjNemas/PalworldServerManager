using MikuLogger;
using PalWorldServerManagerShared.Extensions;
using PalWorldServerManagerShared.Helper;
using PalWorldServerManagerShared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PalworldServerManagerClient.Communication
{
    public class Client
    {
        public static readonly List<Client> ConnectedTcpClients = new List<Client>();
        public bool IsConnected { get; set; }

        private readonly Logger _logger;
        private readonly IncomingDataHandler _incomingDataHandler;
        private readonly TcpClient _tcpClient;

        private IPEndPoint _serverAddress;

        public Client(Logger logger) 
        {
            _logger = logger;
            _tcpClient = new TcpClient();
        }

        public async Task ConnectToServerAsync(IPEndPoint serverAddress, bool withPermaRead = false)
        {
            _serverAddress = serverAddress;
            _logger.LogInfo("Connecting to Server...");
            try
            {
                await _tcpClient.ConnectAsync(_serverAddress);
                IsConnected = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Connection to Server Failed." + ex);
                IsConnected = false;
                return;
            }
            _logger.LogInfo("Connected to Server.");

            if(withPermaRead)
                _ = ReadData();
        }

        private async Task ReadData()
        {
            await using var networkStream = _tcpClient.GetStream();
            var stringBuilder = new StringBuilder();

            while (true)
            {
                if (!_tcpClient.Connected)
                    return;

                var buffer = new byte[1_024];
                int bytesToRead = 0;
                try
                {
                    bytesToRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Lost Connection to Server on reading Data");
                    _logger.LogError(ex);
                    _tcpClient.Close();
                    return;
                }
                var stringData = Encoding.UTF8.GetString(buffer, 0, bytesToRead);
                stringBuilder.Append(stringData);

                if(bytesToRead < buffer.Length)
                {
                    
                    var jsonString = stringBuilder.ToString();
#if DEBUG
                    _logger.LogInfo("Message Recieved: " + jsonString);
#endif
                    stringBuilder.Clear();

                    if (!string.IsNullOrEmpty(jsonString))
                    {
                        var message = JsonSerializer.Deserialize<Message>(jsonString);
                        _incomingDataHandler.ProcessIncomingData(message);
                    }
                }
            }
        }

        public async Task SendDataAsync(Message message)
        {
            if (!_tcpClient.Connected)
                return;

            var jsonString = message.ToJsonString();

            if (jsonString.Count() == 1024)
                jsonString += " ";

            try
            {
                var byteData = Encoding.UTF8.GetBytes(jsonString);
                await _tcpClient.GetStream().WriteAsync(byteData);
            }
            catch (Exception ex)
            {
                _logger.LogError("Lost Connection to Server on Sending Data" + ex);
                _tcpClient.Close();
            }
        }

        public void CloseConnection()
        {
            _tcpClient.Close();
        }
    }
}
