namespace FunPay.Library.Storages
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Код
        /// </summary>
        public string Code { get; set; }

        public string Hashtag { get; set; }
        /// <summary>
        /// Обращение
        /// </summary>
        public string NikName { get; set; }
        /// <summary>
        /// Число лайков
        /// </summary>
        public int Like { get; set; }
        /// <summary>
        /// Процент на списание лайками
        /// </summary>
        public double Percentage { get; set; }
        /// <summary>
        /// Информация
        /// </summary>
        public string Information { get; set; }
        /// <summary>
        /// ссылка на изображение
        /// </summary>
        public string UrlImage { get; set; }
    }
}
