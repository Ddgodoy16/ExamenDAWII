using Agenda.Models;
using Agenda.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Agenda.ViewModel
{
    public class ViewModelRegistro : INotifyPropertyChanged
    {

        public ViewModelRegistro() {

            crearUsuario = new Command(async () => {

                string url = "https://apex.oracle.com/pls/apex/desarrollo_web/usuarios/api_usuarios";

                ConsumoServicios servicio = new ConsumoServicios(url);

                CrearUsuarioBody body = new CrearUsuarioBody() { 
                    
                    correo = correoprivado, 
                  
                    contrasena = contrasena

                };

                CrearUsuarioResponse response = await servicio.PostAsync<CrearUsuarioResponse>(body);

                if (response.mensaje == "Creación Exitosa")
                {
                    var pagina = new InicioSession();
                    
                    Application.Current.MainPage.Navigation.PushAsync(pagina);

                }
                else {
                    Result = response.mensaje;
                }
                

            });

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

    internal class MainPage : Page
    {
    }
}
