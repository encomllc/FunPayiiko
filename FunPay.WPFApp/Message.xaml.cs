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
    /// Логика взаимодействия для Message.xaml
    /// </summary>
    public partial class Message : Window
    {
        public Message()
        {
            InitializeComponent();
        }
        public Message(string text)
        {
            InitializeComponent();
            Text.Text = text;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
