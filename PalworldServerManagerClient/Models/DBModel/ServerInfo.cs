using System.ComponentModel.DataAnnotations;

namespace PalworldServerManagerClient.Models.DBModel
{
    public class ServerInfo
    {
        [Key]
        public int Id { get; set; }
        public string ServerName { get; set; }
        public string IPAdresse { get; set; }
        public int Port { get; set; }
        public bool UsePassword { get; set; }
        public string Password { get; set; }
    }
}
