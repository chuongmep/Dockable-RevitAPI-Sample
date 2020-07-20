using System.Windows;
using Autodesk.Revit.UI;
using MessageBox = System.Windows.Forms.MessageBox;

namespace DockableRevitAPI.Controls
{
    public partial class DocPanel : IDockablePaneProvider
    {
        public DocPanel()
        {
            InitializeComponent();
        }
        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Cancel");
        }

        private void btn_Ok(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("OK");
        }
    }
}