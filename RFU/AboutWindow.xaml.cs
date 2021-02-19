using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace RFU
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
            AboutTextBox.Text = "About: \n\nRF Updater (.NET Core) \nVersion: " + Assembly.GetExecutingAssembly().GetName().Version + "\nMy twitter: https://twitter.com/tunguso4ka \nMy github: https://twitter.com/tunguso4ka \nI <3 Stef \n\nThank you very much for using <3";
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
