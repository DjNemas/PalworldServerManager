using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using PalWorldServerManagerShared.Definitions;
using PalWorldServerManagerShared.Model;
using System.Text.Json;
using System.Threading.Tasks;

namespace PalworldServerManagerClient.Communication
{
    public class IncomingDataHandler
    {
        public void ProcessIncomingData(Message message)
        {
            switch (message.Command)
            {
                case Command.Connect:
                    Connect(message);
                    break;
                default:
                    break;
            }
        }

        private void Connect(Message message)
        {
            //if((bool)message.Data == true)
            //{
            //    ContentDialog dialog = new ContentDialog();

            //    // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            //    dialog.XamlRoot = this.XamlRoot;
            //    dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            //    dialog.Title = "Save your work?";
            //    dialog.PrimaryButtonText = "Save";
            //    dialog.SecondaryButtonText = "Don't Save";
            //    dialog.CloseButtonText = "Cancel";
            //    dialog.DefaultButton = ContentDialogButton.Primary;
            //    dialog.Content = new ContentDialogContent();

            //    var result = await dialog.ShowAsync();
            //}
        }
    }
}
