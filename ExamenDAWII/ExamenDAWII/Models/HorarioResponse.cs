using Agenda.ViewsModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Models
{
   public class HorarioResponse
    
    {
        [JsonProperty("$ref")]
        public string @ref { get; set; }
    }

    public class HorarioItem
    {
        public int id { get; set; }
        public string horario { get; set; }
    }

    public class HorarioRoot
    {
        public List<HorarioItem> items { get; set; }
        public HorarioResponse first { get; set; }
    }
}
