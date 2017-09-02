using System;
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

namespace FunPay.WPFApp
{
    /// <summary>
    /// Логика взаимодействия для NavigationWindow.xaml
    /// </summary>
    public partial class NavigationWindow : Window
    {
        public NavigationWindow()
        {
            InitializeComponent();
        }

        public delegate void ClickButtonDelegate(string button);

        public event ClickButtonDelegate ClicButtonEvent;
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }

        private void Company_Click(object sender, RoutedEventArgs e)
        {
            if (ClicButtonEvent != null)
                ClicButtonEvent("company");
        }

        private void Code_Click(object sender, RoutedEventArgs e)
        {
            if (ClicButtonEvent != null)
                ClicButtonEvent("code");
        }

        private void User_Click(object sender, RoutedEventArgs e)
        {
            if (ClicButtonEvent != null)
                ClicButtonEvent("user");
        }

    }
}
