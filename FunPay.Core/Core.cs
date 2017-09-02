using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using FunPay.Library.Requests;
using FunPay.Library.Storages;
using FunPay.WPFApp;
using Newtonsoft.Json;

namespace FunPay.Core
{
    public class Core
    {

        /// <summary>
        /// Настройки
        /// </summary>
        public Settings Settings { get; set; } = new Settings();
        /// <summary>
        /// Хранилище
        /// </summary>
        public Storage Storage { get; set; } = new Storage();
        /// <summary>
        /// Запросы
        /// </summary>
        public Request Request { get; set; } = new Request();
        /// <summary>
        /// Коллекция для хранения пользователей
        /// </summary>
        public List<User> Users { get; set; } = new List<User>();


        private LoginWindow LoginWindow { get; set; }
        private MainWindow MainWindow { get; set; }
        private UserProfileWindow UserProfileWindow { get; set; }


        /// <summary>
        /// Запуск плагина
        /// </summary>
        public void Start()
        {
            //Запуск навигационной панели
            StartNavWindow();

            if (Storage.ExistSettings())
            {
                //Загружаем настройки из файла
                Settings = JsonConvert.DeserializeObject<Settings>(Storage.GetSettings());
                //Start
            }
            else
            {
                //Запускс окна входа
                StartLoginWindow();

            }
            StartMessage("Привет мир");
        }

        /// <summary>
        /// Событие ввода кода заведения
        /// </summary>
        /// <param name="code"></param>
        private void LoginWindow_EnterCodeEvent(string code)
        {
            var sett = Request.GetCode(code);
            if (sett.Id != null)
            {
                sett.Login = true;
                Settings = sett;
                if (!Storage.Files.ExistFolder("FunPay"))
                {
                    Storage.Files.AddFolder("FunPay");
                }
                Storage.AddSettings(JsonConvert.SerializeObject(Settings));
                LoginWindow.Close();
            }
            else
            {
                //Error Message
                MessageBox.Show("error");
            }
        }
        public delegate void AddDicsontDelegate(int discont);

        public event AddDicsontDelegate AddDiscontEvent;
        /// <summary>
        /// Событие снятия лайков
        /// </summary>
        /// <param name="like"></param>
        private void UserProfileWindow_WithdrawEnterEvent(int like)
        {
            if (Users.Any())
            {
                var user = Users.Last();
                var maxWithdrawLike = user.Like * user.Percentage;
                if (like < maxWithdrawLike)
                {
                    var order = new Order();
                    order.Like = like;
                    order.EstablishmentId = Settings.Id;
                    order.UserId = user.Id;
                    order.Id = Guid.NewGuid().ToString();
                    order.Date = DateTime.Now;

                    WithdrawLike(order);
                    if (AddDiscontEvent != null)
                        AddDiscontEvent(order.Like);
                    UserProfileWindow.Close();


                }
                else
                {
                    MessageBox.Show("error");
                }
            }
            else
            {
                MessageBox.Show("error");
            }


        }
        /// <summary>
        /// Событие ввода кода
        /// </summary>
        /// <param name="code"></param>
        private void MainWindow_SearchEvent(string code)
        {
            var user = Request.GetUser(code);
            if (user.Id != null)
            {
                Users.Add(user);
                MainWindow.Close();

                StartUserProfile(user);
            }
            else
            {
                MessageBox.Show("error");
            }
        }

        /// <summary>
        /// Событие нажатия на кнопки в навигационном меню
        /// </summary>
        /// <param name="button"></param>
        private void NavigationWindow_ClicButtonEvent(string button)
        {
            switch (button)
            {
                case "company":
                {
                    if (Settings.Login)
                        StartCompanyWindow();
                    else
                    {
                        StartLoginWindow();
                    }

                }
                    break;
                case "code":
                {
                    if (Settings.Login)
                        StartMainWindow();
                    else
                    {
                        StartLoginWindow();
                    }
                }
                    break;
                case "user":
                {
                    if (Settings.Login)
                        if (Users.Any())
                            StartUserProfile(Users.Last());
                        else
                        {
                            MessageBox.Show("error");
                        }
                    else
                    {
                        StartLoginWindow();
                    }
                }
                    break;
            }
        }

        //Заглушка для снятия лайков
        public void WithdrawLike(Order order)
        {
            Request.SendOrder(order);
        }

        #region StartingWindows

        private void StartNavWindow()
        {
            var thread = new Thread(new ThreadStart(() =>
            {
                NavigationWindow navigationWindow = new NavigationWindow();
                navigationWindow.ClicButtonEvent += NavigationWindow_ClicButtonEvent;
                navigationWindow.ShowDialog();
            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

        }


        private void StartMainWindow()
        {
            var thread = new Thread(new ThreadStart(() =>
            {
                MainWindow = new MainWindow();
                MainWindow.SearchEvent += MainWindow_SearchEvent;
                MainWindow.ShowDialog();

            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

        }

        private void StartLoginWindow()
        {
            var thread = new Thread(new ThreadStart(() =>
            {
                LoginWindow = new LoginWindow();
                LoginWindow.EnterCodeEvent += LoginWindow_EnterCodeEvent;
                LoginWindow.ShowDialog();
            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void StartCompanyWindow()
        {
            var thread = new Thread(new ThreadStart(() =>
            {

                CompanyWindow companyWindow = new CompanyWindow(Settings.Code, Settings.Name, Settings.Users, Settings.Like);
                companyWindow.ShowDialog();
            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

        }

        private void StartUserProfile(User user)
        {
            var thread = new Thread(new ThreadStart(() =>
            {
                UserProfileWindow = new UserProfileWindow();
                UserProfileWindow.WithdrawEnterEvent += UserProfileWindow_WithdrawEnterEvent;
                UserProfileWindow.InitData(user.Code, user.NikName, user.Like, user.Percentage,  user.UrlImage);
                UserProfileWindow.ShowDialog();

            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

        }

        public void StartMessage(string text)
        {
            var thread = new Thread(new ThreadStart(() =>
            {
               Message message = new Message(text);
                message.ShowDialog();

            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        #endregion
    }
}
