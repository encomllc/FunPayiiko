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
    /// Логика взаимодействия для KeybordControl.xaml
    /// </summary>
    public partial class KeybordControl : UserControl
    {
        public KeybordControl()
        {
            InitializeComponent();
            AddEvents();
        }

        public delegate void NumeralDelegate(string text);
        public delegate void DeleteNumeral();

        public event NumeralDelegate NumeralEvent;
        public event DeleteNumeral DeleteEvent;


        /// <summary>
        /// Добавляем события ко всем кнопкам
        /// </summary>
        public void AddEvents()
        {
            foreach (var mainGridChild in MainGrid.Children)
            {
                if ((mainGridChild as Button).Name != "Del")
                    (mainGridChild as Button).Click += KeybordControl_Click; ;
            }
        }

        private void KeybordControl_Click(object sender, RoutedEventArgs e)
        {
            var text = (sender as Button).Content.ToString();
            if(NumeralEvent != null)
            NumeralEvent(text);
           

        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteEvent != null) DeleteEvent();
        }
    }
}
