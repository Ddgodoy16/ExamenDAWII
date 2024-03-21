using Agenda.View.MasterDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agenda.View.Tabbed
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InicioSession : ContentPage
	{
		public InicioSession ()
		{
			InitializeComponent ();
            
            
        }
        private async void IniciarSesion (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyMasterDetailPage());
        }
    }
}