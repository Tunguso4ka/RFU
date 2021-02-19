using System;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Windows.Controls;
using System.IO;
using System.IO.Compression;
using System.ComponentModel;
using System.Diagnostics;

namespace RFU
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        string GameName;
        Version NewGameVersion;
        Version ThisGameVersion;
        string FolderPath;
        string GamePath;
        string ZipPath;
        string GameUpdateUri;
        string SettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RFUpdater\settings.dat";
        DirectoryInfo FolderPathDirectory;

        public GamePage(string gameName, Version newGameVersion, Version thisGameVersion, string gamePath, string gameUpdateUrl)
        {
            InitializeComponent();
            GameName = gameName;
            GameNameTextBlock.Text = GameName;
            NewGameVersion = newGameVersion;
            NewVersionTextBlock.Text = "New version: " + NewGameVersion;
            ThisGameVersion = thisGameVersion;
            ThisVersionTextBlock.Text = "This version: " + thisGameVersion;
            GamePath = gamePath;
            GameUpdateUri = gameUpdateUrl;
            FolderPath = Properties.Settings.Default.SaveFolderPath + GameName;
            FolderPath = FolderPath.Replace(' ', '_');
            FolderPathDirectory = new DirectoryInfo(FolderPath);
            ProgressBar0.Visibility = Visibility.Hidden;
            DownSpeedTextBlock.Visibility = Visibility.Hidden;
            if (Properties.Settings.Default.GameStatus == 0)
            {
                StatusTextBlock.Text = "Status: Not installed.";
                InstallBtn.Content = "⬇💾Install";
                DeleteBtn.Visibility = Visibility.Hidden;
            }
            else if (Properties.Settings.Default.GameStatus == 1)
            {
                StatusTextBlock.Text = "Status: Installed.";
                InstallBtn.Content = "🎮Play";
                DeleteBtn.Visibility = Visibility.Visible;
            }
            else if (Properties.Settings.Default.GameStatus == 2)
            {
                StatusTextBlock.Text = "Status: Update found.";
                InstallBtn.Content = "🆕Update";
                DeleteBtn.Visibility = Visibility.Visible;
            }
        }

        private void InstallBtn_Click(object sender, RoutedEventArgs e)
        {
            Install();
        }

        void Install()
        {
            if (Properties.Settings.Default.GameStatus == 0)
            {
                Installing();
            }
            else if (Properties.Settings.Default.GameStatus == 1)
            {
                if (File.Exists(GamePath))
                {
                    Process.Start(GamePath);
                }
                else
                {
                    DeleteGame();
                }
            }
            else if (Properties.Settings.Default.GameStatus == 2)
            {

            }
        }

        void Installing()
        {
            Properties.Settings.Default.Installing = true;
            WebClient WebClient = new WebClient();
            if (!FolderPathDirectory.Exists)
            {
                FolderPathDirectory.Create();
            }
            ZipPath = FolderPath + @"\RandomFights.zip";
            GamePath = FolderPath + @"\" + NewGameVersion + @"\RandomFights.exe";

            Uri UpdateUri = new Uri(GameUpdateUri);

            WebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            WebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            WebClient.DownloadFileAsync(UpdateUri, ZipPath);

            InstallBtn.Visibility = Visibility.Hidden;
            ProgressBar0.Visibility = Visibility.Visible;
            DownSpeedTextBlock.Visibility = Visibility.Visible;
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressBar0.Value = e.ProgressPercentage;
            DownSpeedTextBlock.Text = "Bytes received: " + e.BytesReceived;
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                Properties.Settings.Default.GameStatus = 1;
                StatusTextBlock.Text = "Status: Installed.";
                InstallBtn.Content = "🎮Play";
                InstallBtn.Visibility = Visibility.Visible;
                DeleteBtn.Visibility = Visibility.Visible;
                ProgressBar0.Visibility = Visibility.Hidden;
                DownSpeedTextBlock.Visibility = Visibility.Hidden;

                ZipFile.ExtractToDirectory(ZipPath, FolderPath);
                File.Delete(ZipPath);

                BinaryWriter BinaryWriter = new BinaryWriter(File.Open(SettingsPath, FileMode.Create));
                BinaryWriter.Write(Properties.Settings.Default.UpdaterLanguage);
                BinaryWriter.Write(GameName);
                BinaryWriter.Write(Convert.ToString(ThisGameVersion));
                BinaryWriter.Write(GamePath);
                BinaryWriter.Write(Properties.Settings.Default.GameStatus);
                BinaryWriter.Write(Properties.Settings.Default.AutoUpdate);
                BinaryWriter.Write(Properties.Settings.Default.SaveFolderPath);
                BinaryWriter.Dispose();
            }
            Properties.Settings.Default.Installing = false;
        }


        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (FolderPathDirectory.Exists)
            {
                FolderPathDirectory.Delete(true);
                DeleteGame();
            }
            else
            {

            }
        }
        void DeleteGame()
        {
            Properties.Settings.Default.GameStatus = 0;
            StatusTextBlock.Text = "Status: Not installed.";
            InstallBtn.Content = "⬇💾Install";
            DeleteBtn.Visibility = Visibility.Hidden;

            BinaryWriter BinaryWriter = new BinaryWriter(File.Open(SettingsPath, FileMode.Create));
            BinaryWriter.Write(Properties.Settings.Default.UpdaterLanguage);
            BinaryWriter.Write(GameName);
            BinaryWriter.Write(Convert.ToString(ThisGameVersion));
            BinaryWriter.Write(GamePath);
            BinaryWriter.Write(Properties.Settings.Default.GameStatus);
            BinaryWriter.Write(Properties.Settings.Default.AutoUpdate);
            BinaryWriter.Write(Properties.Settings.Default.SaveFolderPath);
            BinaryWriter.Dispose();
        }
    }
}
