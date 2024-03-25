using Agenda.Models;
using Agenda.View;
using Agenda.View.Tabbed;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;



namespace Agenda.ViewsModels
{
    public class VMAgenda : INotifyPropertyChanged
    {
        private string response;

        public ObservableCollection<AgendaItem> ListaAgenda { get; set; } = new ObservableCollection<AgendaItem>();

        public VMAgenda()
        {
            Task.Run(async () => await GetDatos());
            GuardarTarea = new Command(async () => await GuardarTareaAsync());

            EditarTareaCommand = new Command<AgendaItem>(async (agendaItem) => await EditarTareaAsync(agendaItem));

        }

        private async Task GetDatos()
        {
            string url = "https://apex.oracle.com/pls/apex/cardonaapex/api_agenda/app_agenda";
            ConsumoServicios servicios = new ConsumoServicios(url);

            try
            {
                var response = await servicios.Get<AgendaRoot>();

                if (response != null && response.items != null)
                {
                   
                    foreach (var item in response.items)
                    {
                        ListaAgenda.Add(new AgendaItem
                        {
                            id_asignatura = item.id_asignatura,
                            id_horario = item.id_horario,
                            id = item.id,
                            tarea = item.tarea
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos: {ex.Message}");
            }
        }

        private async Task GuardarTareaAsync()
        {
            string url = "https://apex.oracle.com/pls/apex/cardonaapex/api_agenda/app_agenda";
            ConsumoServicios servicio = new ConsumoServicios(url);

            AgendaBody body = new AgendaBody()
            {
                id_asignatura = Id_Asignatura,
                id_horario = Id_Horario,
                tarea = Tarea
            };

            try
            {
                var postResponse = await servicio.PostAsync<AgendaBody>(body);

                response = postResponse.mensaje;

                if (response == "Creación Exitosa")
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "Tarea guardada exitosamente", "OK");
                    await Application.Current.MainPage.Navigation.PushAsync(new ListAgenda());
                }
                else
                {
                    Result = response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar tarea: {ex.Message}");
            }
        }

        public Command GuardarTarea { get; private set; }
        public Command<AgendaItem> EditarTareaCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int id_asignatura;
        public int Id_Asignatura
        {
            get => id_asignatura;
            set
            {
                id_asignatura = value;
                OnPropertyChanged(nameof(Id_Asignatura));
            }
        }

        private int id_horario;
        public int Id_Horario
        {
            get => id_horario;
            set
            {
                id_horario = value;
                OnPropertyChanged(nameof(Id_Horario));
            }
        }

        private string tarea;
        public string Tarea
        {
            get => tarea;
            set
            {
                tarea = value;
                OnPropertyChanged(nameof(Tarea));
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
        private async Task EditarTareaAsync(AgendaItem agendaItem)
        {
            // Verificar que haya un elemento seleccionado para editar
            if (SelectedAgendaItem != null)
            {
                string url = "https://apex.oracle.com/pls/apex/cardonaapex/api_agenda/app_agenda/id";
                ConsumoServicios servicio = new ConsumoServicios(url);

                AgendaBody body = new AgendaBody()
                {
                    id_asignatura = Id_Asignatura,
                    id_horario = Id_Horario,
                    tarea = Tarea
                };

                try
                {
                    
                    var putResponse = await servicio.PutAsync<AgendaBody>(body);


                    response = putResponse.mensaje;

                    if (response == "Edición Exitosa")
                    {
                        await Application.Current.MainPage.DisplayAlert("Success", "Tarea editada exitosamente", "OK");
                        
                        await GetDatos();
                    }
                    else
                    {
                        Result = response;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al editar tarea: {ex.Message}");
                }
            }
            else
            {
              
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor, selecciona una tarea para editar", "OK");
            }
        }

       
        public Command EditarTarea { get; private set; }
        public object SelectedAgendaItem { get; private set; }
    }
}