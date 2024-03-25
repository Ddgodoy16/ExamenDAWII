using Agenda.Models;
using Agenda.View.Tabbed;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;



namespace Agenda.ViewsModels
{
    public class VMHorario
    {
        
       
        public ObservableCollection<HorarioItem>    listaHorario { get; set; } = new ObservableCollection<HorarioItem>();
      

        public VMHorario()
        {
            Task.Run(async () => await GetDatos());
            AgregarHorario = new Command(async () => await AgregarHorarioAsync());
        }

        private async Task GetDatos()
        {
            string url = "https://apex.oracle.com/pls/apex/cardonaapex/api_horairo/app_horario";

            ConsumoServicios servicios = new ConsumoServicios(url);

            try
            {
                var response = await servicios.Get<HorarioRoot>();

                if (response != null && response.items != null)
                {
                    foreach (var item in response.items)
                    {
                        listaHorario.Add(new HorarioItem
                        {
                            id = item.id,
                            horario = item.horario
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private async Task AgregarHorarioAsync()
        {
            string url = "https://apex.oracle.com/pls/apex/cardonaapex/api_horairo/app_horario";
            ConsumoServicios servicio = new ConsumoServicios(url);

            HorarioBody body = new HorarioBody()
            {
                horario = NuevoHorario
            };

            try
            {
                var postResponse = await servicio.PostAsync<HorarioBody>(body);

                response = (string)postResponse.mensaje;

                if (response == "Creación Exitosa")
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "Horario Guardado exitosamente", "OK");
                    await Application.Current.MainPage.Navigation.PushAsync(new HorarioClases());
                }
                else
                {
                    Result = (string)response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar Horario: {ex.Message}");
            }
        }

       

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string nuevohorario;
        public string NuevoHorario
        {
            get => nuevohorario;
            set
            {
                nuevohorario = value;
                OnPropertyChanged(nameof(NuevoHorario));
            }
        }

        private string result;
        private string response;

        public string Result
        {
            get => result;
            set
            {
                result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public Command AgregarHorario { get; }
        
    }
}
