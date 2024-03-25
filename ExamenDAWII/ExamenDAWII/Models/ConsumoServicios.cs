
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
        public string Url { get; set; }

        public ConsumoServicios(string newUrl)
        {
            Url = newUrl;
        }

        public async Task<T> Get<T>()
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(Url);

                if (response.StatusCode == System.Net.HttpStatusCode.OK && response != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
                }
            }
            catch (Exception err)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error de Comunicacion", "Ok");
            }

            return default(T);
        }

        public async Task<T> PostAsync<T>(object obj)
        {
            try
            {
                HttpClient client = new HttpClient();
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                var formData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
                var content = new FormUrlEncodedContent(formData);
                var response = await client.PostAsync(Url, content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK && response != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
                }
            }
            catch (Exception err)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error de Comunicacion", "Ok");
            }

            return default(T);
        }

        public async Task<T> PutAsync<T>(object obj)
        {
            try
            {
                HttpClient client = new HttpClient();
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(Url, content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK && response != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
                }
            }
            catch (Exception err)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error de Comunicacion", "Ok");
            }

            return default(T);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.DeleteAsync($"{Url}?id={id}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
            }
            catch (Exception err)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error de Comunicacion", "Ok");
            }

            return false;
        }
    }
}
