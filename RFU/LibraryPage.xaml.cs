using System;
using System.Collections.Generic;
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
            GamesList();
            /*RFTextBox.Text = GameName;
            try
            {
                RFImage.Source = new BitmapImage(ImageSourceUri);
            }
            catch
            {

            }*/
        }

        private void GamesList()
        {
            List<GameData> ListWithGameData = new List<GameData>();
            ListWithGameData.Add(new GameData() { AGameName = "Random Fights", AGameSource = new Uri("https://drive.google.com/uc?id=1pbvzQhhskJR8Vi-y-rC9AJ4iI1uylJ5g", UriKind.RelativeOrAbsolute), BtnTag = "0" });
            GameItemsControl.ItemsSource = ListWithGameData;
        }

        private void AGameBtn_Click(object sender, RoutedEventArgs e)
        {
            Button PressedButton = (Button)sender;
            if((string)PressedButton.Tag == "0")
            {
                ((MainWindow)Window.GetWindow(this)).Frame0.Content = ((MainWindow)Window.GetWindow(this)).RandomFightsPage;
            }
                
        }
    }

    public class GameData
    {
        public string AGameName { get; set; }
        public Uri AGameSource { get; set; }
        public string BtnTag { get; set; }
    }
}
