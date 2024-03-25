using Agenda.Models;
using Xamarin.Forms;

namespace Agenda.ViewsModels
{
    internal class EditarTareaPage : Page
    {
        private AgendaItem agendaItem;

        public EditarTareaPage(AgendaItem agendaItem)
        {
            this.agendaItem = agendaItem;
        }
    }
}