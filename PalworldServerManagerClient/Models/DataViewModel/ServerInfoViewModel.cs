using PalworldServerManagerClient.Models.DBModel;
using PalworldServerManagerClient.ViewModels;
using PalWorldServerManagerShared.Helper;
using System;
using System.Net;

namespace PalworldServerManagerClient.Models.DataViewModel
{
    public class ServerInfoViewModel : ViewModelBase
    {
        private ServerInfoDB _dbModel;
        public ServerInfoViewModel(ServerInfoDB dbModel) 
        {
            _dbModel = dbModel;
        }

        public bool HasError => HasAnyError();
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
        public string IPAddresse
        {
            get => _dbModel.IPAddresse;
            set
            {
                ClearErrorIPAddress();
                IsDomainOrIPAddress(value);
                _dbModel.IPAddresse = value;
            }
        }

        public bool IpAddresseHasError => !string.IsNullOrEmpty(IpAddresseErrorMessage);
        public string IpAddresseErrorMessage;

        public int Port
        {
            get => _dbModel.Port;
            set => _dbModel.Port = value;
        }

        public bool UsePassword { get; set; }
        public string Password { get; set; }

        public ServerInfoDB GetDbModel() => _dbModel;

        private bool IsDomainOrIPAddress(string userInput)
        {
            if(string.IsNullOrEmpty(userInput))
            {
                SetErrorIPAddress("This field is required.");
                return false;
            }                

            userInput = userInput.Trim();
            if (IPEndPoint.TryParse(userInput, out _))
                return true;
            if (Uri.CheckHostName(userInput) != UriHostNameType.Unknown)
                return true;

            SetErrorIPAddress("Invalid IP or Domain");
            return false;
        }

        private void SetErrorIPAddress(string message)
        {
            IpAddresseErrorMessage = message;
            InvokePropertyChanged(nameof(IpAddresseHasError));
            InvokePropertyChanged(nameof(IpAddresseErrorMessage));
            InvokePropertyChanged(nameof(HasError));
        }

        private void ClearErrorIPAddress()
        {
            IpAddresseErrorMessage = string.Empty;
            InvokePropertyChanged(nameof(IpAddresseHasError));
            InvokePropertyChanged(nameof(HasError));
        }

        private bool HasAnyError()
            => !IpAddresseHasError;
    }
}
