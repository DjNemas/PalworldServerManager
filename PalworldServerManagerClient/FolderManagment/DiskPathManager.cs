using System;
using System.Collections.Generic;
using System.IO;
using static System.Environment;

namespace PalworldServerManagerClient.FolderManagment
{
    public static class DiskPathManager
    {
        private static readonly string currentDirectory = Environment.GetFolderPath(SpecialFolder.LocalApplicationData);

        private static readonly Dictionary<DiskPath, DirectoryInfo> _dirPathList = new()
        {
            { DiskPath.DatabaseFolder, new DirectoryInfo(Path.Combine(currentDirectory, "Database")) },
        };

        private static readonly Dictionary<DiskPath, FileInfo> _folderPathList = new()
        {
            { DiskPath.DatabaseDatabaseFile, new FileInfo(Path.Combine(GetFolderPath(DiskPath.DatabaseFolder).FullName, "Database.db")) }
        };

        public static DirectoryInfo GetFolderPath(DiskPath folder) => _dirPathList[folder];

        public static FileInfo GetFilePath(DiskPath file) => _folderPathList[file];
    }

    public enum DiskPath
    {
        DatabaseFolder,
        DatabaseDatabaseFile
    }
}
