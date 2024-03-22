using Agenda.Models;
using Agenda.View;
using Agenda.View.MasterDetail;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Agenda.View.Tabbed



{
    public class ViewModelInicioSession : INotifyPropertyChanged
    {
        public ViewModelInicioSession()
        {

            autenticacion = new Command(async () => {

                string url = " ";

                ConsumoServicios servicio = new ConsumoServicios(url);

                AuthBody obj = new AuthBody()
                {

                    correo = correoprivado,
                    pass = contrasena
                };

                AuthResponse response = await servicio.PostAsync<AuthResponse>(obj);

                if (response.auth == "1")
                {

                    var pagina = new MyMasterDetailPage();
                    Application.Current.MainPage.Navigation.PushAsync(pagina);

                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Error", "Usuario y Contraseña no coinciden", "Ok");
                }


            });


            crearUsuario = new Command(() => {

                var pagina = new MyMasterDetailPage();
                Application.Current.MainPage.Navigation.PushAsync(pagina);

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

        public Command autenticacion { get; }

        public Command crearUsuario { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}