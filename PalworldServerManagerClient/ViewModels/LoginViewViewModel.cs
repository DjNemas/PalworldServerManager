using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MikuLogger;
using PalworldServerManagerClient.Communication;
using PalworldServerManagerClient.Database;
using PalworldServerManagerClient.Models.DataViewModel;
using PalworldServerManagerClient.Models.DBModel;
using PalWorldServerManagerShared.Helper;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;

namespace PalworldServerManagerClient.ViewModels
{
    public class LoginViewViewModel : ViewModelBase
    {
        private readonly Logger _logger;
        private readonly DatabaseContext _db;
        private readonly Client _client;

        public LoginViewViewModel(Logger logger, DatabaseContext db, Client client)
        {
            _logger = logger;
            _db = db;
            _db.ServerInfos.ForEachAsync(e => ServerInformationList.Add(new ServerInfoViewModel(e))).Wait();
            _client = client;
        }

        private ServerInfoViewModel _serverInformationSelectedItem { get; set; }
        public ObservableCollection<ServerInfoViewModel> ServerInformationList { get; set; } = new();
        public ServerInfoViewModel ServerInformationSelectedItem { 
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

        public async void Add(object sender, RoutedEventArgs e)
        {
            var viewModel = new ServerInfoViewModel(new ServerInfo())
            {
                ServerName = "New Server"
            };

            ServerInformationList.Add(viewModel);
            await _db.ServerInfos.AddAsync(viewModel.GetDbModel());
            await _db.SaveChangesAsync();

        }

        public async void Delete(object sender, RoutedEventArgs e)
        {
            _db.Remove(ServerInformationSelectedItem.GetDbModel());
            await _db.SaveChangesAsync();
            ServerInformationList.Remove(ServerInformationSelectedItem);
        }

        public void Connect(object sender, RoutedEventArgs e)
        {
            
            if (IPEndPoint.TryParse(ServerInformationSelectedItem.IPAddresse, out var ip))
            {
                _client.ConnectToServer(ip);
            }

            var domain = Dns.GetHostAddresses(ServerInformationSelectedItem.IPAddresse);
            if (domain.Count() == 1)
            {
                var newIP = new IPEndPoint(domain.ElementAt(0), ServerInformationSelectedItem.Port);
                _client.ConnectToServer(newIP);
            }

            
        }

        public void OnFocusLost(object sender, RoutedEventArgs e)
        {
            _db.Update(ServerInformationSelectedItem.GetDbModel());
            _db.SaveChanges();
        }

        public void OnPortNumberChanged(object sender, TextBoxBeforeTextChangingEventArgs e)
        {
            var tb = sender as TextBox;
            if(!RegexHelper.IsTextAllowed(e.NewText))
            {
                e.Cancel = true;
                return;
            }

            var success = int.TryParse(e.NewText, out var value);
            if (!success)
            {
                e.Cancel = true;
                return;
            }

            if (value > int.MaxValue || value < int.MinValue)
                e.Cancel = true;
        }
        
    }
}
