using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control_Escolar.Models
{
    public class Localidad
    {
        #region Atributos
        [JsonProperty("id")]
        public int? ID { get; set; }
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        [JsonProperty("idMunicipio")]
        public int IdMunicipio { get; set; }
        public Municipio Municipio { get; set; }
        #endregion
        
    }
}