using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows;

namespace _2025._03._27
{
   public class serverconnection
    {
        HttpClient client = new HttpClient();
        public List<jasondata> all = new List<jasondata>();
        string serverUrl = "";
        public serverconnection(string serverUrl)
        {
            this.serverUrl = serverUrl;
        }
        public async Task<List<jasondata>> Kolbaszok()
        {
           
           
            string url = serverUrl + "/kolbaszok";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
              
                result.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return all;
        }


        public async Task<bool> Register(string name, float rate, int price)
        {
            string url = serverUrl + "/createKolbasz";
            try
            {
                var jsonInfo = new
                {
                    kolbaszNeve = name,
                    kolbaszErtekelese = rate,
                    kolbaszAra = price
                };
                string jsonStringified = JsonConvert.SerializeObject(jsonInfo);
                HttpContent sendThis = new StringContent(jsonStringified, Encoding.UTF8, "Application/json");
                HttpResponseMessage response = await client.PostAsync(url, sendThis);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                jasondata data = JsonConvert.DeserializeObject<jasondata>(result);
               
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            return false;
        }
        public async Task<bool> deleteklbasz(int id)
        {
            string url = serverUrl + "/deleteKolbasz/:id";
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

    }
}
