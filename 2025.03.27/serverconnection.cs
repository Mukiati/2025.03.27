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
            List<jasondata> allkolbi = new List<jasondata>();

            string url = serverUrl + "/kolbaszok";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                allkolbi = JsonConvert.DeserializeObject<List<jasondata>>(result).ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return allkolbi;
        }


        public async Task<bool> Register(string kolbaszNeve,  int kolbaszAra, float kolbaszErtekelese)
        {
            string url = serverUrl + "/createKolbasz";
            try
            {
                var jsonInfo = new
                {
                    kolbaszNeve = kolbaszNeve,
                    kolbaszErtekelese = kolbaszErtekelese,
                    kolbaszAra = kolbaszAra
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
        public async Task<bool> deleteKolbi(int id)
        {
            string url = serverUrl + "/deleteKolbasz/" + id;
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();

                return true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            return false;
        }
    }

}

