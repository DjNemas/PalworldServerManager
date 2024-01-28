namespace MikuLogger
{
    public class Logger
    {
        #region Members
        private FileInfo? _logFileInfo = null;
        private bool _logConsole = true;
        private bool _logFile = false;
        private string _dateTimeFormat = "dd.MM.yyyy HH.mm";
        private bool _useUTCTime = false;
        #endregion

        #region Publics
        public void EnableFileLog(FileInfo logFile)
        {
            _logFileInfo = logFile;
            _logFile = true;

            if (logFile.Directory is null || logFile.DirectoryName is null)
                throw new ArgumentException("Directory cant be null", nameof(logFile.Directory));

            if (!logFile.Directory.Exists)
                Directory.CreateDirectory(logFile.DirectoryName);

            if(!logFile.Exists)
                File.Create(logFile.FullName);
        }

        public void DisableFileLog()
        {
            _logFileInfo = null;
            _logFile= false;
        }

        public void EnableConsoleLog() 
            => _logConsole = true;

        public void DisableConsoleLog() 
            => _logConsole = false;

        public void ChangeDateTimeFormat(string format) 
            => _dateTimeFormat = format;

        public void LogInfo(object message)
        {
            Console.ResetColor();
            Log(LogType.Info, ConvertObjectToString(message));
        }

        public void LogDebug(object message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Log(LogType.Debug, ConvertObjectToString(message));
        }

        public void LogWarning(object message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Log(LogType.Warning, ConvertObjectToString(message));
        }

        public void LogError(object message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Log(LogType.Error, ConvertObjectToString(message));
        }
        #endregion

        #region Privats
        private void Log(LogType logType, string message)
        {
            if (_logConsole)
                LogToConsole(logType, message);
            if (_logFile)
                LogToFile(logType, message);
        }

        private void LogToConsole(LogType logType, string message)
            => Console.Write(BuildMessageString(logType, message));

        private void LogToFile(LogType logType, string message)
#pragma warning disable CS8602 // Dereferenzierung eines möglichen Nullverweises.
            => File.AppendAllText(_logFileInfo.FullName, BuildMessageString(logType, message));
#pragma warning restore CS8602 // Dereferenzierung eines möglichen Nullverweises.

        private string BuildMessageString(LogType logType, string message)
        {
            DateTime dateTime;
            if (_useUTCTime)
                dateTime = DateTime.UtcNow;
            else
                dateTime = DateTime.Now;
            return $"{dateTime.ToString(_dateTimeFormat)} [{logType}] {message} \n";
        }

        private string ConvertObjectToString(object message)
#pragma warning disable CS8603 // Mögliche Nullverweisrückgabe.
            => string.IsNullOrEmpty(message.ToString()) ? string.Empty : message.ToString();
#pragma warning restore CS8603 // Mögliche Nullverweisrückgabe.
        #endregion

        #region Enum
        private enum LogType
        {
            Info,
            Debug,
            Warning,
            Error
        }
        #endregion
    }
}
