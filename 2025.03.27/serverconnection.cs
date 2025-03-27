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
   public  class serverconnection
    {
        HttpClient client = new HttpClient();
        string serverUrl = "";
        public serverconnection(string serverUrl)
        {
            this.serverUrl = serverUrl;
        }
        public async Task<List<string>> Kolbaszok()
        {
            List<string> all = new List<string>();
            string url = serverUrl + "/kolbaszok";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                all = JsonConvert.DeserializeObject<List<jasondata>>(result).Select(item => item.kolbaszNeve).ToList();
                result.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return all;
        }
        public async Task<bool> Register(int kolbaszAra, string kolbaszNeve,float kolbaszErtekelese)
        {
            string url = serverUrl + "/createKolbasz";
            try
            {
                var jsonInfo = new
                {
                    name = kolbaszNeve,
                    ara = kolbaszAra,
                    ertekeles = kolbaszErtekelese
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
                var jsonInfo = new { id= id };
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

    }
}
