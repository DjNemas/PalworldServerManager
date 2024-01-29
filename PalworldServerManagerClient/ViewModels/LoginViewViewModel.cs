using Microsoft.UI.Xaml;
using PalworldServerManagerClient.Models.DataViewModel;
using System.Collections.ObjectModel;

namespace PalworldServerManagerClient.ViewModels
{
    public class LoginViewViewModel : ViewModelBase
    {
        private ServerInformation _serverInformationSelectedItem { get; set; }
        public ObservableCollection<ServerInformation> ServerInformationList { get; set; } = new();
        public ServerInformation ServerInformationSelectedItem { 
            get => _serverInformationSelectedItem;
            set { 
                if(_serverInformationSelectedItem != value)
                {
                    _serverInformationSelectedItem = value;
                    InvokePropertyChanged();
                    InvokePropertyChanged(nameof(IsItemSelected));
                }
            }
        }
        
        public bool IsItemSelected => ServerInformationSelectedItem is not null;

        public void Add(object sender, RoutedEventArgs e)
        {
            ServerInformationList.Add(
                new ServerInformation()
                {
                    ServerName = "New Server"
                }
            );
        }

        public void Delete(object sender, RoutedEventArgs e)
        {
            ServerInformationList.Remove(ServerInformationSelectedItem);
        }

    }
}
