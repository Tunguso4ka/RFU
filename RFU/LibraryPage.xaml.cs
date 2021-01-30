using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RFU
{
    /// <summary>
    /// Interaction logic for LibraryPage.xaml
    /// </summary>
    public partial class LibraryPage : Page
    {
        string GameName;

        public GamePage RandomFightsPage;
        Uri ImageSourceUri = new Uri("https://drive.google.com/uc?id=1pbvzQhhskJR8Vi-y-rC9AJ4iI1uylJ5g", UriKind.RelativeOrAbsolute);
        public LibraryPage(string gameName)
        {
            InitializeComponent();
            GameName = gameName;
            RFTextBox.Text = GameName;
            try
            {
                RFImage.Source = new BitmapImage(ImageSourceUri);
            }
            catch
            {

            }
        }

        private void RandomFightsBtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).Frame0.Content = ((MainWindow)Window.GetWindow(this)).RandomFightsPage;
        }

        private void ClickerBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CalculatorBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
