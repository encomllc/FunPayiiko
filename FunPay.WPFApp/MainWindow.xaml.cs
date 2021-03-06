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
using System.Windows.Shapes;

namespace FunPay.WPFApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Keybord.NumeralEvent += Keybord_NumeralEvent;
            Keybord.DeleteEvent += Keybord_DeleteEvent;
        }

        public delegate void SearchDelegate(string code);

        public event SearchDelegate SearchEvent;
        private void Keybord_DeleteEvent()
        {
            if(MainTextBox.Text.Length>0)
           MainTextBox.Text= MainTextBox.Text.Substring(0, MainTextBox.Text.Length - 1);
        }

        private void Keybord_NumeralEvent(string text)
        {
            MainTextBox.Text += text;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (SearchEvent != null)
                SearchEvent(MainTextBox.Text);
        }

        private void Сancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
