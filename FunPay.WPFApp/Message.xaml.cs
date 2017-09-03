using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

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
            Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
            {
                try
                {
                    
                        Text.Text = text;
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }));



        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
