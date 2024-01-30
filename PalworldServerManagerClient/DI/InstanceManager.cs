using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MikuLogger;
using PalworldServerManagerClient.Communication;
using PalworldServerManagerClient.Database;
using PalworldServerManagerClient.FolderManagment;
using PalworldServerManagerClient.UserControls.Login;
using PalworldServerManagerClient.ViewModels;
using PalworldServerManagerClient.Views;
using PalWorldServerManagerShared.Definitions;
using System.IO;
using System.Net;

namespace PalworldServerManagerClient.DI
{
    internal class InstanceManager : IInstanceManager
    {
        public static InstanceManager Instance;

        private ServiceProvider _serviceProvider;

        public InstanceManager()
        {
            Instance = this;
            _serviceProvider = CreateServices();
        }

        public ServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddSingleton<Logger>()
                .AddSingleton<App>()
                .AddSingleton<LoginViewViewModel>()
                .AddTransient<ServerList>()
                .AddTransient<LoginView>()
                .AddTransient<LoginView>()
                .AddTransient<Client>()
                .AddDbContext<DatabaseContext>(SetDBOptions)
                .BuildServiceProvider();
        }

        public T GetInstance<T>() where T : class => _serviceProvider.GetRequiredService<T>();

        public Client StartClient(IPEndPoint endpoint) => new Client(GetInstance<Logger>(), endpoint);

        private void SetDBOptions(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            var logger = new Logger();
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.LogTo(logger.LogInfo, LogLevel.Information, DbContextLoggerOptions.SingleLine | DbContextLoggerOptions.Level | DbContextLoggerOptions.Category);
            optionsBuilder.LogTo(logger.LogInfo, LogLevel.Debug | LogLevel.Trace, DbContextLoggerOptions.SingleLine | DbContextLoggerOptions.Level | DbContextLoggerOptions.Category);
            optionsBuilder.LogTo(logger.LogInfo, LogLevel.Warning, DbContextLoggerOptions.SingleLine | DbContextLoggerOptions.Level | DbContextLoggerOptions.Category);
            optionsBuilder.LogTo(logger.LogInfo, LogLevel.Error | LogLevel.Critical, DbContextLoggerOptions.SingleLine | DbContextLoggerOptions.Level | DbContextLoggerOptions.Category);
#endif
            optionsBuilder.UseSqlite($"Data Source={DiskPathManager.GetFilePath(DiskPath.DatabaseDatabaseFile).FullName}");

            var test2 = DiskPathManager.GetFilePath(DiskPath.DatabaseDatabaseFile).DirectoryName;
            var test = Directory.Exists(DiskPathManager.GetFilePath(DiskPath.DatabaseDatabaseFile).DirectoryName);
            if (!Directory.Exists(DiskPathManager.GetFilePath(DiskPath.DatabaseDatabaseFile).DirectoryName))
                Directory.CreateDirectory(DiskPathManager.GetFilePath(DiskPath.DatabaseDatabaseFile).DirectoryName);
        }
    }
}
