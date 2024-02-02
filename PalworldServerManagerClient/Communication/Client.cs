﻿using MikuLogger;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PalworldServerManagerClient.Communication
{
    public class Client
    {
        private Logger _logger;

        private IPEndPoint _serverAddress;
        private readonly TcpClient _tcpClient;

        public Client(Logger logger) 
        {
            _logger = logger;
            _tcpClient = new TcpClient();
        }

        public void ConnectToServer(IPEndPoint serverAddress)
        {
            _serverAddress = serverAddress;
            _logger.LogInfo("Connecting to Server...");
            try
            {
                _tcpClient.Connect(_serverAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError("Connection to Server Failed." + ex);
                return;
            }
            _logger.LogInfo("Connected to Server.");
            ReadData();
        }

        private async void ReadData()
        {
            if (!_tcpClient.Connected)
                return;

            await using var networkStream = _tcpClient.GetStream();
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
                    _logger.LogError("Lost Connection to Server on reading Data");
                    _logger.LogError(ex);
                    _tcpClient.Close();
                    return;
                }
                var stringData = Encoding.UTF8.GetString(buffer, 0, bytesToRead);
                stringBuilder.Append(stringData);

                if(bytesToRead < buffer.Length)
                {
                    var message = stringBuilder.ToString();
                    _logger.LogInfo("Message Recieved: " + message);
                    _logger.LogInfo("Message Lenght: " + message.Count());
                    stringBuilder.Clear();
                }
            }
        }

        public async void SendData(string message)
        {
            if (!_tcpClient.Connected)
                return;

            if (message.Count() == 1024)
                message += " ";
            var byteData = Encoding.UTF8.GetBytes(message);
            try
            {
                await _tcpClient.GetStream().WriteAsync(byteData);
            }
            catch (Exception ex)
            {
                _logger.LogError("Lost Connection to Server on Sending Data" + ex);
                _tcpClient.Close();
            }
        }
    }
}
