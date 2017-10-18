using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FunPay.Library.Storages
{
    public class Places
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("hashtags")]
        public List<string> Hashtags { get; set; } = new List<string>();
        [JsonProperty("users")]
        public int Users { get; set; }
        [JsonProperty("like")]
        public int Like { get; set; }
        [JsonProperty("percentage")]
        public double Percentage { get; set; }
    }
}
