using MikuLogger;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace PalworldServerManagerServer.Installation
{
    internal class PalworldInstallManager
    {
        private readonly Logger _logger;
        private readonly SteamCmd _steamCmd;

        public PalworldInstallManager(Logger logger, SteamCmd steamCmd) 
        {
            _logger = logger;
            _steamCmd = steamCmd;
        }

        public void DownloadServer()
        {

        }

        public async Task<bool> NewVersionAvailable()
        {
            if (await GetOnlineBuildId() != await GetLocalBuildID())
                return true;
            else
                return false;
        }

        public async Task<string?> GetLocalBuildID()
        {
            var fileInfo = GetLocalMetaDataFile();
            if (fileInfo is null)
                return null;

            var jsonSting = await File.ReadAllLinesAsync(fileInfo.FullName);
            var buildIdLine = jsonSting.FirstOrDefault(x => x.Contains("buildid"));
            if (buildIdLine is not null)
                return buildIdLine?.Split("\"")[3];
            else
                return null;
        }

        private async Task<string?> GetOnlineBuildId()
        {
            using var httpClient = new HttpClient();
            var streamResponse = await httpClient.GetStreamAsync("https://api.steamcmd.net/v1/info/2394010");
            var jsonNode = await JsonSerializer.DeserializeAsync<JsonNode>(streamResponse);

            if (jsonNode is null)
                return null;

            string? buildId;
            try
            {
                buildId = (string?)jsonNode["data"]?["2394010"]?["depots"]?["branches"]?["public"]?["buildid"];
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get buildId number.");
                _logger.LogError(ex);
                return null;
            }
            return buildId;
        }

        private DirectoryInfo GetPalworldServerDir()
            => new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "PalworldServer"));

        private FileInfo GetLocalMetaDataFile()
            => new FileInfo(Path.Combine(GetPalworldServerDir().FullName, "steamapps", "appmanifest_2394010.acf"));
    }
}
