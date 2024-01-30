using Microsoft.EntityFrameworkCore;
using MikuLogger;
using PalworldServerManagerClient.Models.DBModel;

namespace PalworldServerManagerClient.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options, Logger logger) : base (options) { }

        public DbSet<ServerInfo> ServerInfos { get; set; }
    }
}
