using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace Agenda.ViewsModel
{
   
        public partial class VMAsignatura : ContentPage
        {
            private const string apiUrl = "https://apex.oracle.com/pls/apex/cardonaapex/api_asignatura/app_asignatura";

            private HttpClient httpClient;

            public ObservableCollection<Asignatura> Asignaturas { get; set; }

        public VMAsignatura()
            {
           
                Asignaturas = new ObservableCollection<Asignatura>();
                BindingContext = this;
                httpClient = new HttpClient();
                CargarAsignaturas();
            }


        private async void CargarAsignaturas()
            {
                try
                {
                    var json = await httpClient.GetStringAsync(apiUrl);
                    var asignaturas = JsonConvert.DeserializeObject<ObservableCollection<Asignatura>>(json);
                    foreach (var asignatura in asignaturas)
                    {
                        Asignaturas.Add(asignatura);
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    Console.WriteLine("Error al cargar asignaturas: " + ex.Message);
                }
            }

            private async void CrearAsignatura(Asignatura nuevaAsignatura)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(nuevaAsignatura);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(apiUrl, content);
                    response.EnsureSuccessStatusCode();
                    // Si se ha creado correctamente, puedes actualizar la lista de asignaturas
                    CargarAsignaturas();
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    Console.WriteLine("Error al crear asignatura: " + ex.Message);
                }
            }

            private async void ActualizarAsignatura(Asignatura asignaturaActualizada)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(asignaturaActualizada);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await httpClient.PutAsync(apiUrl + "/" + asignaturaActualizada.Id, content);
                    response.EnsureSuccessStatusCode();
                    // Si se ha actualizado correctamente, puedes actualizar la lista de asignaturas
                    CargarAsignaturas();
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    Console.WriteLine("Error al actualizar asignatura: " + ex.Message);
                }
            }

            private async void EliminarAsignatura(int idAsignatura)
            {
                try
                {
                    var response = await httpClient.DeleteAsync(apiUrl + "/" + idAsignatura);
                    response.EnsureSuccessStatusCode();
                    // Si se ha eliminado correctamente, puedes actualizar la lista de asignaturas
                    CargarAsignaturas();
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    Console.WriteLine("Error al eliminar asignatura: " + ex.Message);
                }
            }
        }

        public class Asignatura
        {
            public int Id { get; set; }
            public string nombre { get; set; }
            public string descripcion { get; set; }
            // Añade otras propiedades según la estructura de tus datos
        }
    }
