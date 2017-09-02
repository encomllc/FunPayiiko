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
    /// Логика взаимодействия для UserProfileWindow.xaml
    /// </summary>
    public partial class UserProfileWindow : Window
    {
        public UserProfileWindow()
        {
            InitializeComponent();
            Keybord.NumeralEvent += Keybord_NumeralEvent;
            Keybord.DeleteEvent += Keybord_DeleteEvent;
        }

        public delegate void WithdrawEnterDelegate(int like);

        public event WithdrawEnterDelegate WithdrawEnterEvent;
       
        public void InitData(string code, string nikName, int like, double percentage, 
            string urlImage)
        {
           
            NikName.Content = nikName;
            Like.Content = like;
            Available.Content = like * percentage;
           

            
        }

        private void Keybord_DeleteEvent()
        {
            if (MainTextBox.Text.Length > 0)
                MainTextBox.Text = MainTextBox.Text.Substring(0, MainTextBox.Text.Length - 1);
        }

        private void Keybord_NumeralEvent(string text)
        {
            MainTextBox.Text += text;
        }

        private void Сancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            var like = MainTextBox.Text;

            if(WithdrawEnterEvent!=null)
            WithdrawEnterEvent(Convert.ToInt32(like));
        }
    }
}
