using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Agenda.View.Tabbed;
using Xamarin.Essentials;

namespace Agenda.View.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyMasterDetailPageMaster : ContentPage
    {
        public ListView ListView;

        public MyMasterDetailPageMaster()
        {
            InitializeComponent();
            BindingContext = new MyMasterDetailPageMasterViewModel();
            ListView = MenuItemsListView;
        }



        class MyMasterDetailPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MyMasterDetailPageMenuItem> MenuItems { get; set; }

            public MyMasterDetailPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MyMasterDetailPageMenuItem>(new[]
                {
                 new MyMasterDetailPageMenuItem { Id = 0, Title = "Inicio", TargetType = typeof(Inicio), },
                 new MyMasterDetailPageMenuItem { Id = 1, Title = "Horario Clases", TargetType = typeof(HorarioClases), },
                 new MyMasterDetailPageMenuItem { Id = 2, Title = "Agregar Horario", TargetType = typeof(FormHorario), },
                 new MyMasterDetailPageMenuItem { Id = 3, Title = "Calendario", TargetType = typeof(Calendario), },
                 new MyMasterDetailPageMenuItem { Id = 4, Title = "Agregar Agenda", TargetType = typeof(FormAgenda), },
                 new MyMasterDetailPageMenuItem { Id = 5, Title = "Agenda", TargetType = typeof(ListAgenda), },
                 new MyMasterDetailPageMenuItem { Id = 6, Title = "Agregar Asignaturas", TargetType = typeof(Asignatura), },
                 new MyMasterDetailPageMenuItem { Id = 7, Title = "Asignaturas", TargetType = typeof(List_Asignaturas), },
                 });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}
