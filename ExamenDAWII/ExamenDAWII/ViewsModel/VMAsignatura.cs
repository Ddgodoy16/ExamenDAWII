using Agenda.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Agenda.ViewsModel
{
    public class VMAsignatura
    {
        public VMAsignatura()
        {
            Task.Run(async () => await GetDatos());
        }

        private async Task GetDatos()
        {
            string url = "https://apex.oracle.com/pls/apex/cardonaapex/api_asignatura/app_asignatura";
            ConsumoServicios servicios = new ConsumoServicios(url);

            try
            {
                var response = await servicios.Get<Root>();

                if (response != null && response.items != null)
                {
                    foreach (var item in response.items)
                    {
                        listaAsignatura.Add(new Item { id = item.id, nombre = item.nombre });
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public ObservableCollection<Item> listaAsignatura { get; set; } = new ObservableCollection<Item>();
    }
}
