using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunPay.Library.Storages;

namespace FunPay.Library.Requests
{
    public class Request
    {
        /// <summary>
        /// Получить инфомация о заведении
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Settings GetCode(string code)
        {
            return new Settings() { Code = code, Id = "Demo",Name = "Adsdfds",Like = 100, Users = 5, Version = "5" };
        }

        public User GetUser(string code)
        {
            if (code == "0000")
            {
                return new User() { NikName = "Alexander Popov", Code = "0000", Id = "08163243", Percentage = 0.3, Like = 500, UrlImage = "https://maxcdn.icons8.com/Share/icon/Users//circled_user_female1600.png" };
            }
            else
            {
                return new User();
            }
          
        }

        public void SendOrder(Order order)
        {
            
        }
    }
}
