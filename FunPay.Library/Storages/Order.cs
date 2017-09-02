using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunPay.Library.Storages
{
    public class Order
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Идентификатор заведения
        /// </summary>
        public string EstablishmentId { get; set; }
        /// <summary>
        /// Количество списываемых лайков
        /// </summary>
        public int Like { get; set; }
        public DateTime Date { get; set; }
        /// <summary>
        /// Json Объект Ordr iiko
        /// </summary>
        public string OrderData { get; set; }
    }
}
