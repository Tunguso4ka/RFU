﻿using System;
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
        string SettingsPath = Properties.Settings.Default.AppDataPath + "gamesonthispc.dat";
        int GameStatus;
        int ATag;
        int ThisUserLikeNum = 0;
        DirectoryInfo FolderPathDirectory;

        public GamePage(string gameName, Version newGameVersion, Version thisGameVersion, string gamePath, string gameUpdateUrl, int gameStatus, int tag)
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
            GameStatus = gameStatus;
            ATag = tag;

            FolderPath = Properties.Settings.Default.SaveFolderPath + GameName;
            FolderPath = FolderPath.Replace(' ', '_');
            FolderPathDirectory = new DirectoryInfo(FolderPath);
            ProgressBar0.Visibility = Visibility.Hidden;
            DownSpeedTextBlock.Visibility = Visibility.Hidden;

            if (GameStatus == 0)
            {
                StatusTextBlock.Text = "Status: Not installed.";
                InstallBtn.Content = "⬇💾Install";
                InstallBtn.ToolTip = "Install";
                DeleteBtn.Visibility = Visibility.Hidden;
            }
            else if (GameStatus == 1)
            {
                StatusTextBlock.Text = "Status: Installed.";
                InstallBtn.Content = "🎮Play";
                InstallBtn.ToolTip = "Play";
                DeleteBtn.Visibility = Visibility.Visible;
            }
            else if (GameStatus == 2)
            {
                StatusTextBlock.Text = "Status: Update found.";
                InstallBtn.Content = "🆕Update";
                InstallBtn.ToolTip = "Update";
                DeleteBtn.Visibility = Visibility.Visible;
            }

            //MessageBox.Show("" + GameUpdateUri);
        }

        private void InstallBtn_Click(object sender, RoutedEventArgs e)
        {
            Install();
        }

        void Install()
        {
            if (Properties.Settings.Default.GameStatus == 0)
            {
                MessageBox.Show("Installing");
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
                try
                {
                    MessageBox.Show("Complete");

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
                    int i = 0;
                    while (i != 99)
                    {
                        if (i == ATag)
                        {
                            BinaryWriter.Write(NewGameVersion + "}" + GamePath);
                        }
                        else
                        {
                            BinaryWriter.Write(((MainWindow)Window.GetWindow(this)).SavedGamesVersions[i] + "}" + ((MainWindow)Window.GetWindow(this)).SavedGamesPaths[i]);
                        }
                        i++;
                    }
                    //MessageBox.Show(((MainWindow)Window.GetWindow(this)).SavedGamesVersions[0] + "}" + ((MainWindow)Window.GetWindow(this)).SavedGamesPaths[0]);
                    BinaryWriter.Dispose();
                }
                catch
                {
                    MessageBox.Show("Error: Can't download this app, try later.", "Error");
                }
            }
            Properties.Settings.Default.Installing = false;
            Properties.Settings.Default.Save();
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
            int i = 0;
            while (i != 99)
            {
                if (i == ATag)
                {
                    BinaryWriter.Write("" + "}" + "");
                }
                else
                {
                    BinaryWriter.Write(((MainWindow)Window.GetWindow(this)).SavedGamesVersions[i] + "}" + ((MainWindow)Window.GetWindow(this)).SavedGamesPaths[i]);
                }
                i++;
            }
            BinaryWriter.Dispose();
        }

        private void LikeBtn_Click(object sender, RoutedEventArgs e)
        {
            //like  
            ThisUserLikeNum = 1;
            LikeBtn.Content = "";
            DisLikeBtn.Content = "";
        }

        private void DisLikeBtn_Click(object sender, RoutedEventArgs e)
        {
            //dislike 
            ThisUserLikeNum = 2;
            LikeBtn.Content = "";
            DisLikeBtn.Content = "";
        }
        //
        private void GameInfoHideBtn_Click(object sender, RoutedEventArgs e)
        {
            if(GameInfoStackPanel.Visibility == Visibility.Collapsed)
            {
                GameInfoStackPanel.Visibility = Visibility.Visible;
                GameInfoHideBtn.ToolTip = "Hide info";
            }
            else
            {
                GameInfoStackPanel.Visibility = Visibility.Collapsed;
                GameInfoHideBtn.ToolTip = "Show info";
            }
        }
    }
}
