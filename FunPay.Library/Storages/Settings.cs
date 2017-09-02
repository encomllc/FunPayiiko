using System.Collections.Generic;

namespace FunPay.Library.Storages
{
    public class Settings
    {
        /// <summary>
        /// Идентификатор компании
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Статус входа
        /// </summary>
        public bool Login { get; set; }
        /// <summary>
        /// Код
        /// </summary>
        public string Code { get; set; }

        public string Name { get; set; }

        public List<string> Hashtags { get; set; }

        /// <summary>
        /// Версия 
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Число пользователей
        /// </summary>
        public int Users { get; set; }
        /// <summary>
        /// Число лайков
        /// </summary>
        public int Like { get; set; }
    }
}
