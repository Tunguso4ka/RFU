using Microsoft.Win32;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace RFU
{
    /// <summary>
    /// Interaction logic for UpdateCheckerWindow.xaml
    /// </summary>
    public partial class UpdateCheckerWindow : Window
    {
        string InfoBarText;
        public UpdateCheckerWindow()
        {
            InitializeComponent();
            //DataContext = ;

            InfoBarText = "Start";
            try
            {
                RegistryKey regKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
                regKey.SetValue("RFU Updater Startup", Assembly.GetExecutingAssembly().Location);
                regKey.Close();
                InfoBarText = "Success!";
            }
            catch
            {
                InfoBarText = "Error. Cant set registry key.";
            }
            InfoBarTextBlock.Text = InfoBarText;

            this.Hide();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

    }
}
