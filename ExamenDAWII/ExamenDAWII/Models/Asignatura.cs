
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
namespace Agenda.Models
{
    public class Asignatura
    {
        [JsonProperty("$ref")]
        public string @ref { get; set; }
    }

    public class Item
    {
        internal int id_asignatura;
        internal int id_horario;
        internal string tarea;

        public int id { get; set; }
        public string nombre { get; set; }
    }

    public class Root
    {
        public List<Item> items { get; set; }
        public Asignatura first { get; set; }
    }
}