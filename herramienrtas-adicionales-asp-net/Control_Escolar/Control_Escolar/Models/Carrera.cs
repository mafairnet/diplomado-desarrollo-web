using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control_Escolar.Models
{
    public class Carrera
    {
        #region Atributos
        [JsonProperty("id")]
        public int? ID { get; set; }
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        [JsonProperty("idStatus")]
        public int IdStatus { get; set; }
        public Status Status { get; set; }
        #endregion
    }
}