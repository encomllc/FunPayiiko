using Newtonsoft.Json;

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
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// Код
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }


        /// <summary>
        /// Обращение
        /// </summary>
        [JsonProperty("nikName")]
        public string NikName { get; set; }
        /// <summary>
        /// Число лайков
        /// </summary>
        [JsonProperty("like")]
        public int Like { get; set; }

        /// <summary>
        /// ссылка на изображение
        /// </summary>
        [JsonProperty("urlImage")]
        public string UrlImage { get; set; }
    }
}
