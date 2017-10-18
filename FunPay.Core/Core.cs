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
        public Core()
        {
            this.ChangesScreen += Core_ChangesScreen;
        }


        private bool _isScreen = false;
        

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
        private NavigationWindow NavigationWindow { get; set; }

        public delegate void ChangeScreenDelegate();

        public event ChangeScreenDelegate ChangesScreen;
        

        public bool IsScreen
        {
            get { return _isScreen; }
            set
            {
                _isScreen = value;
                if (ChangesScreen != null)
                    ChangesScreen();
            }
        }

        private void Core_ChangesScreen()
        {
            try
            {
                if(NavigationWindow!=null)
               NavigationWindow.StateWindows(IsScreen);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show(e.Source);
                
            }
           
        }

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
                //Settings= Request.GetCode(set.Code);

                //Start
            }
            else
            {
                //Запускс окна входа
                StartLoginWindow("Введите код заведения");

            }

        }

        /// <summary>
        /// Событие ввода кода заведения
        /// </summary>
        /// <param name="code"></param>
        private void LoginWindow_EnterCodeEvent(string code)
        {
            if (Settings.Login)
            {
                Settings.Password = code;
                Settings.IsPassword = true;
                Storage.AddSettings(JsonConvert.SerializeObject(Settings));
                LoginWindow.Close();
            }
            else
            {

                var sett = Request.GetCode(code);
                if (sett != null)
                {
                    sett.Login = true;
                    Settings = sett;
                    if (!Storage.Files.ExistFolder("FunPay"))
                    {
                        Storage.Files.AddFolder("FunPay");
                    }
                    Storage.AddSettings(JsonConvert.SerializeObject(Settings));
                    LoginWindow.Close();
                    StartLoginWindow("Введите код системного администратора для автоматического добавления скидки");
                }
                else
                {
                    //Error Message
                    StartMessage("Заведения с таким кодом не существует");
                }
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
                var maxWithdrawLike = user.Like * Settings.Percentage;
                if (like <= maxWithdrawLike)
                {
                    var order = new Order();
                    order.Like = like;
                    order.Placeid = Settings.Code;
                    order.UserId = user.Code;
                    order.Id = Guid.NewGuid().ToString();
                    order.Date = DateTime.Now;

                   var request= Request.SendOrder(order);
                    if (request)
                    {
                        user.Like -= order.Like;
                        StartMessage("Списание лайков прошло успешно");
                    }
                    else
                    {
                        StartMessage("Ошибка списания лайков");
                    }
                    if (AddDiscontEvent != null)
                        AddDiscontEvent(order.Like);
                    UserProfileWindow.Close();


                }
                else
                {
                    StartMessage("Нельзя списывать больше допустимого");
                }
            }
            else
            {
                StartMessage("Пожалуйста выберите пользователя");
            }



        }
        /// <summary>
        /// Событие ввода кода
        /// </summary>
        /// <param name="code"></param>
        private void MainWindow_SearchEvent(string code)
        {
            var user = Request.GetUser(code,Settings.Code);
            if (user != null)
            {
                Users.Add(user);
                MainWindow.Close();

                StartUserProfile(user);
            }
            else
            {
                StartMessage("Пользоватея с таким кодом не существует");
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
                            StartLoginWindow("Введите код заведения");
                        }

                    }
                    break;
                case "code":
                    {
                        if (IsScreen)
                            if (Settings.Login)
                                StartMainWindow();
                            else
                            {
                                StartLoginWindow("Введите код заведения");
                            }
                        else
                        {
                            StartMessage("Пожалуйста перейдите на главный экран и повторите операцию. Списание лайков можно производить только на главном экране.");
                        }
                    }
                    break;
                case "user":
                    {
                        if (IsScreen)
                            if (Settings.Login)
                            if (Users.Any())
                                StartUserProfile(Users.Last());
                            else
                            {
                                StartMessage("Вы ещё не добавляли пользователей");
                            }
                        else
                        {
                            StartLoginWindow("Введите код заведения");
                        }
                        else
                        {
                            StartMessage("Пожалуйста перейдите на главный экран и повторите операцию. Списание лайков можно производить только на главном экране.");
                        }
                    }
                    break;
            }
        }

      
       




        #region StartingWindows

        private void StartNavWindow()
        {

            var thread = new Thread(new ThreadStart(() =>
            {
                NavigationWindow = new NavigationWindow
                {
                    Topmost = true
                };
                NavigationWindow.ClicButtonEvent += NavigationWindow_ClicButtonEvent;
                NavigationWindow.ShowDialog();
            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

        }


        private void StartMainWindow()
        {
            var thread = new Thread(new ThreadStart(() =>
            {
                MainWindow = new MainWindow();
                MainWindow.Topmost = true;
                MainWindow.SearchEvent += MainWindow_SearchEvent;
                MainWindow.ShowDialog();

            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

        }

        private void StartLoginWindow(string text)
        {
            var thread = new Thread(new ThreadStart(() =>
            {
                LoginWindow = new LoginWindow(text);
                LoginWindow.Topmost = true;
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
                CompanyWindow companyWindow = new CompanyWindow(Settings.Code, Settings.Name, Settings.Users, Settings.Like,Settings.Hashtags);
                companyWindow.Topmost = true;
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
                UserProfileWindow.Topmost = true;
                UserProfileWindow.WithdrawEnterEvent += UserProfileWindow_WithdrawEnterEvent;
                UserProfileWindow.InitData(user.Code, user.NikName, user.Like, Settings.Percentage, user.UrlImage);
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
                message.Topmost = true;
                message.ShowDialog();

            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        #endregion
    }
}
