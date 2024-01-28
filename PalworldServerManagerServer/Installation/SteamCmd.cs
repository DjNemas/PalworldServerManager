using MikuLogger;
using PalworldServerManagerServer.FolderManagment;
using System.Diagnostics;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace PalworldServerManagerServer.Installation
{
    internal class SteamCmd
    {
        private readonly Logger _logger;
        private readonly DirectoryInfo _steamCmdDir = new DirectoryInfo("steamcmd");
        private readonly Uri _steamCmdDownloadUrl = new Uri("https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip");
        private readonly FileInfo _binary = new FileInfo("steamcmd.exe");

        private Process? _steamCmdProcess;

        public SteamCmd (Logger logger)
        {
            _logger = logger;
        }

        public async Task<bool> Download()
        {
            if (SteamCmdExist())
                return false;

            _logger.LogInfo("Start Downloading Steamcmd");
            CreateFolder();
            try
            {
                await DownloadSteamCmd();
            }
            catch (Exception) 
            {
                _logger.LogError("Error occured while downloading steamcmd");
                return false;
            }
            _logger.LogInfo("Steamcmd Downloaded.");
            return true;
        }

        public bool SteamCmdExist()
        {
            var binaryPath = GetSteamCmdBinaryPath();
            if (File.Exists(binaryPath.FullName))
            {
                _logger.LogInfo("steamcmd.exe already exist. Skip Download");
                return true;
            }
            return false;
        }

        public void StartExecuteBinary(List<string>? args = null)
        {
            var processStartInfo = new ProcessStartInfo(GetSteamCmdBinaryPath().FullName);
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.RedirectStandardError = true;

            if (args is not null && args.Count() > 0)
                args.ForEach(processStartInfo.ArgumentList.Add);

            _steamCmdProcess = new Process() { StartInfo = processStartInfo };
            _steamCmdProcess.OutputDataReceived += OutputDataCallback;
            _steamCmdProcess.ErrorDataReceived += ErrorDataReceived;

            _steamCmdProcess.Start();

            _steamCmdProcess.BeginOutputReadLine();
            _steamCmdProcess.BeginErrorReadLine();
        }

        public void StopExecuteBinary()
        {
            if (_steamCmdProcess is null)
                return;

            _steamCmdProcess.CancelOutputRead();
            _steamCmdProcess.CancelErrorRead();
            _steamCmdProcess.Close();
        }

        private void ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            _logger.LogError("[Steamcmd] " +  e.Data);
        }

        private void OutputDataCallback(object sender, DataReceivedEventArgs e)
        {
            _logger.LogInfo("[Steamcmd] " + e.Data);
        }

        private void CreateFolder()
        {
            var downloadPath = GetSteamCmdFolderPath();
            if (!Directory.Exists(downloadPath.FullName))
                Directory.CreateDirectory(downloadPath.FullName);
        }

        private async Task DownloadSteamCmd()
        {
            using var client = new HttpClient();
            var data = await client.GetByteArrayAsync(_steamCmdDownloadUrl);
            UnzipSteamCmd(data);
        }

        private void UnzipSteamCmd(byte[] data)
        {
            var memoryData = new MemoryStream(data);
            ZipFile.ExtractToDirectory(memoryData, GetSteamCmdFolderPath().FullName);
        }

        private FileInfo GetSteamCmdBinaryPath()
            => new FileInfo(Path.Combine(Folders.GetFolderPath(Folder.Binary).FullName, _steamCmdDir.Name, _binary.Name));

        private DirectoryInfo GetSteamCmdFolderPath()
            => new DirectoryInfo(Path.Combine(Folders.GetFolderPath(Folder.Binary).FullName, _steamCmdDir.Name));
    }
}
