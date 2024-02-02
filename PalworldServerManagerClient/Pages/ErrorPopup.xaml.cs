using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace PalworldServerManagerClient.Pages
{
    public sealed partial class ErrorPopup : Page
    {
        public ErrorPopup()
        {
            InitializeComponent();
        }

        public void SetErrorMessage(string message)
            => tb_ErrorMessage.Text = message;

        public void SetTitle(string title)
            => tb_Title.Text = title;

        public void SetTitleColor(Brush brush)
            => tb_Title.Foreground = brush;
    }
}
