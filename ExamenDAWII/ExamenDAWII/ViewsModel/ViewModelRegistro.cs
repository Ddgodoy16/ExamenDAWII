using Agenda.Models;
using Agenda.View.Tabbed;
using htt.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace htt.ViewModels
{
    public class ViewModelRegistro : INotifyPropertyChanged
    {

        public ViewModelRegistro() {

            crearUsuario = new Command(async () => {

                string url = "https://apex.oracle.com/pls/apex/frlopez0801/api_artes_marciales/user";

                ConsumoServicios servicio = new ConsumoServicios(url);

                CrearUsuarioBody body = new CrearUsuarioBody() { 
                    
                    correo = correoprivado, 
                    nombre = nombre, 
                    pass = contrasena

                };

                CrearUsuarioResponse response = await servicio.PostAsync<CrearUsuarioResponse>(body);

                if (response.mensaje == "Creación Exitosa")
                {
                    var pagina = new Agenda.View.InicioSession();
                    Application.Current.MainPage.Navigation.PushAsync(pagina);

                }
                else {
                    Result = response.mensaje;
                }
                

            });

        }




        string nombre;

        public string Nombre
        {

            get => nombre;
            set
            {
                nombre = value;
                var args = new PropertyChangedEventArgs(nameof(Nombre));
                PropertyChanged?.Invoke(this, args);
            }
        }


        string correoprivado;

        public string Correo
        {

            get => correoprivado;
            set
            {
                correoprivado = value;
                var args = new PropertyChangedEventArgs(nameof(Correo));
                PropertyChanged?.Invoke(this, args);
            }
        }

        string contrasena;

        public string Contrasena
        {

            get => contrasena;
            set
            {
                contrasena = value;
                var args = new PropertyChangedEventArgs(nameof(Contrasena));
                PropertyChanged?.Invoke(this, args);
            }
        }

        string result;

        public string Result
        {

            get => result;
            set
            {
                result = value;
                var args = new PropertyChangedEventArgs(nameof(Result));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public Command crearUsuario { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
