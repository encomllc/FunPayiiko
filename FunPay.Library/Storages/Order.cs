using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FunPay.Library.Storages
{
    public class Order
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [JsonProperty("userid")]
        public string UserId { get; set; }
        /// <summary>
        /// Идентификатор заведения
        /// </summary>
        [JsonProperty("placeid")]
        public string Placeid { get; set; }
        /// <summary>
        /// Количество списываемых лайков
        /// </summary>
        [JsonProperty("like")]
        public int Like { get; set; }
        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
}
