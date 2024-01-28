using MikuLogger;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PalworldServerManagerServer.Communication
{
    internal class ClientListener
    {
        private Logger _logger;
        private TcpListener _listener;

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
            SendData(client, "Connection Successfull");
            ReadData(client);

            WaitForClients();
        }

        private async void SendData(TcpClient client, string message)
        {
            if (message.Count() == 1024)
                message += " ";
            var byteToSend = Encoding.UTF8.GetBytes(message);

            try
            {
                await client.GetStream().WriteAsync(byteToSend);
            }
            catch (Exception)
            {
                _logger.LogInfo("Client Lost Connection");
                client.Close();
                return;
            }
        }

        private async void ReadData(TcpClient client)
        {
            await using var networkStream = client.GetStream();
            var stringBuilder = new StringBuilder();

            while (true)
            {
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
                    var message = stringBuilder.ToString();
                    _logger.LogInfo("Message Recieved: " + message);
                    stringBuilder.Clear();
                    SendData(client, message);
                }
            }
        }
    }
}
