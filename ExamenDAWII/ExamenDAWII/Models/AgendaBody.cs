using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Models
{
    public class AgendaBody
    {
        public string mensaje { get; set; } 

        public int id_asignatura { get; set; }
        public string tarea { get; set; }
        public int id_horario { get; set; }

        
    }
}
