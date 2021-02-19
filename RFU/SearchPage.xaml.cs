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
    }
}
