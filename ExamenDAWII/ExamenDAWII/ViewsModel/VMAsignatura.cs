using Agenda.Models;
using Agenda.View.Tabbed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Agenda.ViewsModels
{
    public class VMAsignatura
    {
        public ObservableCollection<Item> listaAsignatura { get; set; } = new ObservableCollection<Item>();

        public VMAsignatura()
        {
            Task.Run(async () => await GetDatos());
            GuardarAsignatura = new Command(async () => await GuardarAsignaturaAsync());
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
        private async Task GuardarAsignaturaAsync()
        {
            string url = "https://apex.oracle.com/pls/apex/cardonaapex/api_asignatura/app_asignatura";
            ConsumoServicios servicio = new ConsumoServicios(url);

            AsignaturaBody body = new AsignaturaBody()
            {
                nombre = NombreAsignatura
            };

            try
            {
                var postResponse = await servicio.PostAsync<AsignaturaBody>(body);

                response = postResponse.mensaje;

                if (response == "Creación Exitosa")
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "Asignatura guardada exitosamente", "OK");
                    await Application.Current.MainPage.Navigation.PushAsync(new List_Asignaturas());
                }
                else
                {
                    Result = response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar asignatura: {ex.Message}");
            }
        }

        public Command GuardarAsignatura { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string nombreAsignatura;
        public string NombreAsignatura
        {
            get => nombreAsignatura;
            set
            {
                nombreAsignatura = value;
                OnPropertyChanged(nameof(NombreAsignatura));
            }
        }

        private string result;
        public string Result
        {
            get => result;
            set
            {
                result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        private string response;
    }
    }
