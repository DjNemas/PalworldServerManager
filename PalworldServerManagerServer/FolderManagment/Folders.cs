using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalworldServerManagerServer.FolderManagment
{
    internal static class Folders
    {
        private static Dictionary<Folder, DirectoryInfo> folderDic = new()
        {
            { Folder.Binary, new DirectoryInfo("Binary") },
            { Folder.PalworldServer, new DirectoryInfo("PalwoldServer") } 
        };

        public static DirectoryInfo GetFolderPath(Folder folder)
            => new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, folderDic[folder].Name));
    }

    internal enum Folder
    {
        Binary,
        PalworldServer,
    }
}
