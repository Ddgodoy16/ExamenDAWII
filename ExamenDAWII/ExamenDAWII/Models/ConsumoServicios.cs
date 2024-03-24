
using System;
using System.Collections.Generic;
using System.Net.Cache;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Agenda.Models
{
    public class ConsumoServicios
    {
        public string url {get;set;}

        public ConsumoServicios(string newUrl) { 
            
            url = newUrl;
        }

        public async Task<T> Get<T>(){

            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);

                if(response.StatusCode == System.Net.HttpStatusCode.OK && response != null ) { 

                    var jsonString = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);

                }

            }
            catch (Exception err) {
                Application.Current.MainPage.DisplayAlert("Error", "Error de Comunicacion", "Ok");
            }

            return default(T);
        }

        public async Task<T> PostAsync<T>(Object obj) {

            try
            {
                HttpClient client = new HttpClient();
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                var formData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary <string, string>>(jsonData);
                var content = new FormUrlEncodedContent(formData);
                var response = await client.PostAsync(url, content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK && response != null)
                {

                    var jsonString = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);

                }

            }
            catch (Exception err) {

                Application.Current.MainPage.DisplayAlert("Error", "Error de Comunicacion", "Ok");
            }

            
            return default(T);
        }


    }
}
