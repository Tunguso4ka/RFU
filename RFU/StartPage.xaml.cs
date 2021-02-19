using System.Windows;
using System.Windows.Controls;

namespace RFU
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
            if (Properties.Settings.Default.UpdaterLanguage == "ru")
            {
                if (Properties.Settings.Default.GameStatus == 0)
                {
                    GameStatusTextBlock.Text = "Random Fights: Не установлена. Это хороший шанс попробовать её.";
                }
                else if (Properties.Settings.Default.GameStatus == 1)
                {
                    GameStatusTextBlock.Text = "Random Fights: Установлена. Прекрасный день чтобы поиграть.";
                }
                else if (Properties.Settings.Default.GameStatus == 2)
                {
                    GameStatusTextBlock.Text = "Random Fights: Обновление найдено. Скорее попробуйте новые возможности!";
                }
            }
            else if (Properties.Settings.Default.UpdaterLanguage == "en")
            {
                if (Properties.Settings.Default.GameStatus == 0)
                {
                    GameStatusTextBlock.Text = "Random Fights: Not installed. Its good chance to play new game.";
                }
                else if (Properties.Settings.Default.GameStatus == 1)
                {
                    GameStatusTextBlock.Text = "Random Fights: Installed. Nice day to play again.";
                }
                else if (Properties.Settings.Default.GameStatus == 2)
                {
                    GameStatusTextBlock.Text = "Random Fights: Update found. ";
                }
            }
        }
    }
}
