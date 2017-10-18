using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FunPay.Library.Storages;
using Newtonsoft.Json;

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
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = System.Text.Encoding.UTF8;
                    var json = wc.DownloadString("http://dev.funpay.money/api/v1/places/" + code);
                    var places = JsonConvert.DeserializeObject<Places>(json);
                    var settings = new Settings(){Code = places.Code,Id = places.Id,Hashtags = places.Hashtags,Like = places.Like,Name = places.Name,Percentage = places.Percentage,Users = places.Users};
                    return settings;
                }
            }
            catch (Exception e)
            {
                return null;
            }
           
        }

        public User GetUser(string code,string codePlaces)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = System.Text.Encoding.UTF8;
                    var json = wc.DownloadString(String.Format("http://dev.funpay.money/api/v1/places/{0}/client/{1}",codePlaces,code));
                    var user = JsonConvert.DeserializeObject<User>(json);
                    return user;
                }
            }
            catch (Exception e)
            {
                return null;
            }
          
        }

        public bool SendOrder(Order order)
        {

            try
            {
                //var httpWebRequest = (HttpWebRequest)WebRequest.Create(String.Format("http://dev.funpay.money/api/v1/places/{0}/client/{1}", order.Placeid, order.UserId));
                //httpWebRequest.ContentType = "application/json";
                //httpWebRequest.Method = "POST";
                //using (var streamWriter = new

                //    StreamWriter(httpWebRequest.GetRequestStream()))
                //{
                   

                //    streamWriter.Write(JsonConvert.SerializeObject(order));
                //}
                //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                //{
                //    var result = streamReader.ReadToEnd();
                //}
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(order);
                    var response = client.PostAsync(String.Format("http://dev.funpay.money/api/v1/places/{0}/client/{1}", order.Placeid, order.UserId),
                        new StringContent(json, Encoding.UTF8, "application/json"));
                    var res = response.Result;
                    if (res.StatusCode== HttpStatusCode.OK)
                        return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }
    }
}
