using Agenda.Models;
using Agenda.View;
using Agenda.View.MasterDetail;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Agenda.ViewModel
{
    public class Login : INotifyPropertyChanged
    {
        public Login()
        {
            autenticacion = new Command(async () => {
                string url = "https://apex.oracle.com/pls/apex/desarrollo_web/usuarios/auth";

                try
                {
                    ConsumoServicios servicio = new ConsumoServicios(url);

                    AuthBody obj = new AuthBody()
                    {
                        correo = Correo,
                        contrasena = Contrasena
                    };

                    AuthResponse response = await servicio.PostAsync<AuthResponse>(obj);

                    if (response.auth == "1")
                    {
                        var pagina = new MyMasterDetailPage();
                        await Application.Current.MainPage.Navigation.PushAsync(pagina);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Usuario y Contraseña no coinciden", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    Console.WriteLine($"Error: {ex.Message}");
                    await Application.Current.MainPage.DisplayAlert("Error", "Ocurrió un error durante la autenticación", "Ok");
                }
            });

            crearUsuario = new Command(() => {
                var pagina = new ViewRegistro();
                Application.Current.MainPage.Navigation.PushAsync(pagina);
            });
        }

        private string _correo;
        public string Correo
        {
            get => _correo;
            set
            {
                _correo = value;
                OnPropertyChanged(nameof(Correo));
            }
        }

        private string _contrasena;
        public string Contrasena
        {
            get => _contrasena;
            set
            {
                _contrasena = value;
                OnPropertyChanged(nameof(Contrasena));
            }
        }

        private string _result;
        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public Command autenticacion { get; }

        public Command crearUsuario { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
