using System.Windows;
using System.Windows.Controls;

namespace RFU
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        private void ClearSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
        }

        private void StartSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text != "")
            {

            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text != "")
            {
                ClearSearchBtn.Visibility = Visibility.Visible;
            }
            else
            {
                ClearSearchBtn.Visibility = Visibility.Collapsed;
            }
        }

        private void AGameBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
