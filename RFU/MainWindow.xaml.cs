using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace RFU
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RFUpdater\";
        string SettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RFUpdater\settings.dat";
        string RFUUpdateInfoUrl = @"https://drive.google.com/uc?export=download&id=1oKyTppE7V8E-Q0UF0_SXNmW3diQ0QbLJ";
        string RFUUpdateInfoPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RFUpdater\RFUV.txt";
        string Game0pdateInfoPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RFUpdater\RFV.txt";
        string Game0UpdateInfoUrl = @"https://drive.google.com/uc?export=download&id=1-vMmTTLHl7z3O-cAMD_ubUeW1WJyL5ID";
        string Game0Name;
        string Game0Path;
        string Game0UpdateUrl = @"https://drive.google.com/uc?export=download&confirm=no_antivirus&id=1kMrTP1cCcUwDVvQCOYi-7Qs-f9htvpm9";
        Version newGameVersion;
        Version thisGameVersion;
        Version NewRFUVersion;

        //pages
        public SettingsPage ASettingsPage;
        public StartPage AStartPage;
        public LibraryPage ALibraryPage;
        public SearchPage ASearchPage;
        public UserPage AUserPage;
        public GamePage RandomFightsPage;
        public LoginPage ALoginPage;


        //ints

        //strings

        public MainWindow()
        {
            InitializeComponent();

            //UpdateCheckerWindow updateCheckerWindow = new UpdateCheckerWindow();
            //updateCheckerWindow.Show();

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            Checks();
            Pages();

            Frame0.Content = AStartPage;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        void Pages()
        {
            string gameName = Game0Name, GamePath = Game0Path, GameUpdateUrl = Game0UpdateUrl;

            AStartPage = new StartPage();
            ASettingsPage = new SettingsPage(gameName, thisGameVersion, GamePath, GameUpdateUrl);
            ALibraryPage = new LibraryPage(gameName);
            ASearchPage = new SearchPage();
            AUserPage = new UserPage();
            ALoginPage = new LoginPage();

            RandomFightsPage = new GamePage(gameName, newGameVersion, thisGameVersion, GamePath, GameUpdateUrl);
        }

        void Checks()
        {
            SettingsSearch();
            UpdatesChecking();
            BetaCheck();
            AuthorizCheck();
        }

        void SettingsSearch()
        {
            if (File.Exists(SettingsPath))
            {
                try
                {
                    BinaryReader BinaryReader = new BinaryReader(File.OpenRead(SettingsPath));
                    Properties.Settings.Default.UpdaterLanguage = BinaryReader.ReadString();
                    Game0Name = BinaryReader.ReadString();
                    thisGameVersion = new Version(BinaryReader.ReadString());
                    Game0Path = BinaryReader.ReadString();
                    Properties.Settings.Default.GameStatus = BinaryReader.ReadInt32();
                    Properties.Settings.Default.AutoUpdate = BinaryReader.ReadBoolean();
                    Properties.Settings.Default.SaveFolderPath = BinaryReader.ReadString();
                    BinaryReader.Dispose();
                }
                catch
                {
                    Game0Name = "Random Fights";
                    Properties.Settings.Default.GameStatus = 0;
                }
            }
            else
            {
                Game0Name = "Random Fights";
                Game0Path = "";
                Properties.Settings.Default.GameStatus = 0;
            }

        }
        void UpdatesChecking()
        {
            try
            {
                if (File.Exists(RFUUpdateInfoPath))
                {
                    File.Delete(RFUUpdateInfoPath);
                }
                if (File.Exists(Game0pdateInfoPath))
                {
                    File.Delete(Game0pdateInfoPath);
                }
                WebClient WebClient = new WebClient();
                WebClient.DownloadFile(RFUUpdateInfoUrl, RFUUpdateInfoPath);
                using (StreamReader StreamReader = new StreamReader(RFUUpdateInfoPath))
                {
                    NewRFUVersion = new Version(StreamReader.ReadLine());
                    StreamReader.Dispose();
                }
                File.Delete(RFUUpdateInfoPath);
                WebClient.DownloadFile(Game0UpdateInfoUrl, Game0pdateInfoPath);

                using (StreamReader StreamReader = new StreamReader(Game0pdateInfoPath))
                {
                    newGameVersion = new Version(StreamReader.ReadLine());
                    Game0UpdateUrl = StreamReader.ReadLine();
                    StreamReader.Dispose();
                }
                File.Delete(Game0pdateInfoPath);

            }
            catch
            {

            }
        }

        void BetaCheck()
        {
            //Properties.Settings.Default.IsBetaOn
        }

        void AuthorizCheck()
        {
            //Properties.Settings.Default.UserAuthorizited
            if(Properties.Settings.Default.UserAuthorizited == true)
            {
                UserBtn.Content = "";
                UserBtn.ToolTip = Properties.Settings.Default.UserName;
            }
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame0.Content = ASettingsPage;
        }

        private void LibraryBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame0.Content = ALibraryPage;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            if(Properties.Settings.Default.Installing == false)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("You can't close Updater while he doing hes work.");
            }
        }

        private void MenuBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame0.Content = AStartPage;
        }

        private void MinimBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void UserBtn_Click(object sender, RoutedEventArgs e)
        {
            if(Properties.Settings.Default.UserAuthorizited == true)
            {
                Frame0.Content = AUserPage;
            }
            else
            {
                Frame0.Content = ALoginPage;
            }
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame0.Content = ASearchPage;
        }
    }
}
