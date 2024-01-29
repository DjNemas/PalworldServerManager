using Microsoft.UI.Xaml;
using PalworldServerManagerClient.Models;
using System.Collections.ObjectModel;

namespace PalworldServerManagerClient.ViewModels
{
    public class LoginViewViewModel
    {
        public ObservableCollection<ServerInformation> ServerInformationList { get; set; } = new();

        public ServerInformation ServerInformationSelectedItem { get; set; }

        public void Add(object sender, RoutedEventArgs e)
        {
            ServerInformationList.Add(
                new ServerInformation()
                {
                    ServerName = "Test"
                }
            );
        }
    }
}
