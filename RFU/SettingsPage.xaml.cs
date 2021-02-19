using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace RFU
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        AboutWindow AboutWindow = new AboutWindow();
        string GameName;
        Version GameVersion;
        string GamePath;
        string GameUpdateUrl;
        string SettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RFUpdater\settings.dat";
        bool LogOutPressed;
        public SettingsPage(string gameName, Version gameVersion, string gamePath, string gameUpdateUrl)
        {
            InitializeComponent();
            GameName = gameName;
            GameVersion = gameVersion;
            GamePath = gamePath;
            GameUpdateUrl = gameUpdateUrl;
            if (Properties.Settings.Default.UpdaterLanguage == "en")
            {
                LRB0.IsChecked = true;
            }
            else
            {
                LRB1.IsChecked = true;
            }
            if (Properties.Settings.Default.AutoUpdate == true)
            {
                CB0.IsChecked = true;
            }
            if (Properties.Settings.Default.SaveFolderPath == @"C:\Games\")
            {
                LRB2.IsChecked = true;
            }
            else
            {
                LRB3.IsChecked = true;
            }
            if(Properties.Settings.Default.ThemeNum == 0)
            {
                LRB6.IsChecked = true;
            }
            else if (Properties.Settings.Default.ThemeNum == 1)
            {
                LRB4.IsChecked = true;
            }
            else
            {
                LRB5.IsChecked = true;
            }
            if(Properties.Settings.Default.UserAuthorizited == false)
            {
                LogOutBtn.IsEnabled = false;
            }
        }

        private void SaveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (LRB0.IsChecked == true)
            {
                Properties.Settings.Default.UpdaterLanguage = "en";
            }
            else
            {
                Properties.Settings.Default.UpdaterLanguage = "ru";
            }

            if (CB0.IsChecked == true)
            {
                Properties.Settings.Default.AutoUpdate = true;
            }
            else
            {
                Properties.Settings.Default.AutoUpdate = false;
            }

            if (LRB2.IsChecked == true)
            {
                Properties.Settings.Default.SaveFolderPath = @"C:\Games\";
            }
            else
            {
                Properties.Settings.Default.SaveFolderPath = @"D:\Games\";
            }

            BinaryWriter BinaryWriter = new BinaryWriter(File.Open(SettingsPath, FileMode.Create));
            BinaryWriter.Write(Properties.Settings.Default.UpdaterLanguage);
            BinaryWriter.Write(GameName);
            BinaryWriter.Write(Convert.ToString(GameVersion));
            BinaryWriter.Write(GamePath);
            BinaryWriter.Write(Properties.Settings.Default.GameStatus);
            BinaryWriter.Write(Properties.Settings.Default.AutoUpdate);
            BinaryWriter.Write(Properties.Settings.Default.SaveFolderPath);
            BinaryWriter.Dispose();
            Properties.Settings.Default.Save();

            new MainWindow().Show();
            Window.GetWindow(this).Close();
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LogOutPressed == true)
            {
                Properties.Settings.Default.UserAuthorizited = true;
                LogOutBtn.Content = "Log out";
                LogOutPressed = false;
            }
            else
            {
                Properties.Settings.Default.UserAuthorizited = false;
                LogOutBtn.Content = "Cancel";
                LogOutPressed = true;
            }
        }

        private void AboutBtn_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow.Show();
        }
    }
}
