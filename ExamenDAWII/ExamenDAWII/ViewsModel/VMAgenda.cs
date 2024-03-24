using Agenda.Models;
using Agenda.View.Tabbed;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.ViewsModel
{
    public class VMAgenda
    {
        public VMAgenda()
        {
            Task.Run(async () => await GetDatos());
        }

        private async Task GetDatos()
        {
            string url = "https://apex.oracle.com/pls/apex/cardonaapex/api_agenda/app_agenda";

            {
                ConsumoServicios servicios = new ConsumoServicios(url);

                try
                {
                    var response = await servicios.Get<AgendaRoot>();

                    if (response != null && response.items != null)
                    {
                        foreach (var item in response.items)
                        {
                            listaAgenda.Add(new AgendaItem
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
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

            public ObservableCollection<AgendaItem> listaAgenda { get; set; } = new ObservableCollection<AgendaItem>();
        }
    }