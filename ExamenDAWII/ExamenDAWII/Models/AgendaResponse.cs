using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Models
{
    public class AgendaResponse
    {
        [JsonProperty("$ref")]
        public string @ref { get; set; }
    }

    public class AgendaItem
    {
        public int id { get; set; }
        public int id_asignatura { get; set; }
        public string tarea { get; set; }
        public int id_horario { get; set; }
    }

    public class AgendaRoot
    {
        public List<AgendaItem> items { get; set; } // Corrección: Usa AgendaItem en lugar de Item
        public AgendaResponse first { get; set; }
    }
}