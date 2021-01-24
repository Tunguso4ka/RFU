﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RFU
{
    /// <summary>
    /// Interaction logic for LibraryPage.xaml
    /// </summary>
    public partial class LibraryPage : Page
    {
        string GameName;

        public GamePage RandomFightsPage;
        public LibraryPage(string gameName)
        {
            InitializeComponent();
            GameName = gameName;
            GameNameTextBox.Text = GameName;
        }

        private void RandomFightsBtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).Frame0.Content = ((MainWindow)Window.GetWindow(this)).RandomFightsPage;
        }
    }
}
