using Agenda.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;



namespace Agenda.ViewsModel
    {
        public class VMHorario
    {
            public VMHorario()
            {
                Task.Run(async () => await GetDatos());
            }

            private async Task GetDatos()
            {
                string url = "https://apex.oracle.com/pls/apex/cardonaapex/api_horairo/app_horario";

                {
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
                                    horario = item.horario,
                                 
                                 
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

            public ObservableCollection<HorarioItem> listaHorario { get; set; } = new ObservableCollection<HorarioItem>();
        }
    }