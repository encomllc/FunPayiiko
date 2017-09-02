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
using System.Windows.Shapes;

namespace FunPay.WPFApp
{
    /// <summary>
    /// Логика взаимодействия для CompanyWindow.xaml
    /// </summary>
    public partial class CompanyWindow : Window
    {
        public CompanyWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="users"></param>
        /// <param name="likes"></param>
        public CompanyWindow(string code, string name,int users,int likes)
        {
            InitializeComponent();
            Name.Content = name;
            Code.Content = code;
            Users.Content = users;
            Likes.Content = likes;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
