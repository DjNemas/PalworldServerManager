using PalworldServerManagerClient.ViewModels;

namespace PalworldServerManagerClient.Models.DataViewModel
{
    public class ServerInformation : ViewModelBase
    {
        private string serverName;
        private string iPAdresse;
        private int port;
        private bool usePassword;
        private string password;

        public string ServerName
        {
            get => serverName;
            set
            {
                if (serverName != value)
                {
                    serverName = value;
                    InvokePropertyChanged();
                }
            }
        }
        public string IPAdresse
        {
            get => iPAdresse;
            set
            {
                if (serverName != value)
                {
                    serverName = value;
                    InvokePropertyChanged();
                }
            }
        }
        public int Port
        {
            get => port;
            set
            {
                if (port != value)
                {
                    port = value;
                    InvokePropertyChanged();
                }
            }
        }
        public bool UsePassword
        {
            get => usePassword;
            set
            {
                if (usePassword != value)
                {
                    usePassword = value;
                    InvokePropertyChanged();
                }
            }
        }
        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    InvokePropertyChanged();
                }
            }
        }
    }
}
