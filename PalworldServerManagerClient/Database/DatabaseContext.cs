using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using PalworldServerManagerClient.FolderManagment;
using PalworldServerManagerClient.Models.DBModel;
using System.IO;

namespace PalworldServerManagerClient.Database
{
    public class DatabaseContext : DbContext
    {   
       public DbSet<ServerInfoDB> ServerInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            var logger = new MikuLogger.Logger();
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.LogTo(logger.LogInfo, LogLevel.Information, DbContextLoggerOptions.SingleLine | DbContextLoggerOptions.Level | DbContextLoggerOptions.Category);
            optionsBuilder.LogTo(logger.LogInfo, LogLevel.Debug | LogLevel.Trace, DbContextLoggerOptions.SingleLine | DbContextLoggerOptions.Level | DbContextLoggerOptions.Category);
            optionsBuilder.LogTo(logger.LogInfo, LogLevel.Warning, DbContextLoggerOptions.SingleLine | DbContextLoggerOptions.Level | DbContextLoggerOptions.Category);
            optionsBuilder.LogTo(logger.LogInfo, LogLevel.Error | LogLevel.Critical, DbContextLoggerOptions.SingleLine | DbContextLoggerOptions.Level | DbContextLoggerOptions.Category);
#endif
            var test = DiskPathManager.GetFilePath(DiskPath.DatabaseDatabaseFile).FullName;
            optionsBuilder.UseSqlite($"Data Source={DiskPathManager.GetFilePath(DiskPath.DatabaseDatabaseFile).FullName}");

            if (!Directory.Exists(DiskPathManager.GetFilePath(DiskPath.DatabaseDatabaseFile).DirectoryName))
                Directory.CreateDirectory(DiskPathManager.GetFilePath(DiskPath.DatabaseDatabaseFile).DirectoryName);
        }
    }
}
