using PalworldServerManagerClient.Models.DBModel;
using PalworldServerManagerClient.ViewModels;

namespace PalworldServerManagerClient.Models.DataViewModel
{
    public class ServerInfoViewModel : ViewModelBase
    {
        private ServerInfo _dbModel;
        public ServerInfoViewModel(ServerInfo dbModel) 
        {
            _dbModel = dbModel;
        }

        public string ServerName
        {
            get => _dbModel.ServerName;
            set
            {
                if (_dbModel.ServerName != value)
                {
                    _dbModel.ServerName = value;
                    InvokePropertyChanged();
                }
            }
        }
        public string IPAdresse
        {
            get => _dbModel.IPAdresse;
            set
            {
                if (_dbModel.IPAdresse != value)
                {
                    _dbModel.IPAdresse = value;
                    InvokePropertyChanged();
                }
            }
        }
        public int Port
        {
            get => _dbModel.Port;
            set
            {
                if (_dbModel.Port != value)
                {
                    _dbModel.Port = value;
                    InvokePropertyChanged();
                }
            }
        }
        public bool UsePassword
        {
            get => _dbModel.UsePassword;
            set
            {
                if (_dbModel.UsePassword != value)
                {
                    _dbModel.UsePassword = value;
                    InvokePropertyChanged();
                }
            }
        }
        public string Password
        {
            get => _dbModel.Password;
            set
            {
                if (_dbModel.Password != value)
                {
                    _dbModel.Password = value;
                    InvokePropertyChanged();
                }
            }
        }

        public ServerInfo GetDbModel() => _dbModel;
    }
}
