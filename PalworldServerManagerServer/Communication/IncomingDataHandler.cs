using PalworldServerManagerServer.Definistions;
using PalWorldServerManagerShared.Definitions;
using PalWorldServerManagerShared.Model;
using System.Net.Sockets;
using System.Text.Json;

namespace PalworldServerManagerServer.Communication
{
    public static class IncomingDataHandler
    {
        public static void HandleWelcomeClients(TcpClient client, Message message)
        {
            if (message.Command == Command.Connect)
            {
                if(message.JsonData is not null)
                {
                    var connection = JsonSerializer.Deserialize<Connection>(message.JsonData);
                    if (connection is not null)
                    {
                        if(connection.ClientType == 1)
                            ClientListener.SetReadonlyClient(client);
                    }
                }
            }
        }

        public static async void ProcessIncomingData(Message message)
        {
            

        }
    }
}
