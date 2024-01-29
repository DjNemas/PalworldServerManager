namespace PalworldServerManagerClient.Models
{
    public class ServerInformation
    {
        public string ServerName { get; set; }
        public string IPAdresse { get; set; }
        public int Port { get; set; }
        public bool UsePassword { get; set; }
        public string Password { get; set; }

    }
}
