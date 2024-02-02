using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;

namespace PalworldServerManagerClient.Models.DBModel
{
    public class ServerInfo
    {
        [Key]
        public int Id { get; set; }
        public string ServerName { get; set; }
        public string IPAddresse { get; set; }
        public int Port { get; set; }
    }
}
