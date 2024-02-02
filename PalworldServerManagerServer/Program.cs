using Microsoft.Extensions.DependencyInjection;
using MikuLogger;
using PalworldServerManagerServer.DI;
using PalworldServerManagerServer.Installation;

namespace PalworldServerManagerServer
{
    internal class Program
    {
        private static SteamCmd? _steamcmd;

        static void Main(string[] args) => 
            MainAsync(args).GetAwaiter().GetResult();

        static async Task MainAsync(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            var diService = new InstanceManager();
            _steamcmd = diService.GetInstance<SteamCmd>();

            //await _steamcmd.Download();

            //var argList = new List<string>
            //{
            //    "+force_install_dir",
            //    "./PalworldServer",
            //    "+login",
            //    "anonymous",
            //    "+app_update",
            //    "2394010",
            //    "validate",
            //    "+quit"
            //};
            //_steamcmd.StartExecuteBinary(argList);

            var clientListener = diService.StartClientListener(3939);
            clientListener.Start();

            var logger = diService.GetInstance<Logger>();
            logger.LogInfo("Server Application");

            await Task.Delay(-1);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if(_steamcmd is not null)
                _steamcmd.StopExecuteBinary();
        }
    }
}
