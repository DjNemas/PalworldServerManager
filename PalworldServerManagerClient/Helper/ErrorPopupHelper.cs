using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using PalworldServerManagerClient.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalworldServerManagerClient.Helper
{
    public static class ErrorPopupHelper
    {
        public static async Task ShowErrorPopup(XamlRoot root, string title, string message)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = root;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.CloseButtonText = "Cancel";
            var content = new ErrorPopup();
            content.SetErrorMessage(message);
            content.SetTitle(title);
            dialog.Content = content;

            await dialog.ShowAsync();
        }
    }
}
