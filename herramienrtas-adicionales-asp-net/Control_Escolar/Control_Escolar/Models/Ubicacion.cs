using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control_Escolar.Models
{
    public class Ubicacion
    {
        #region Atributos
        [JsonProperty("id")]
        public int? ID { get; set; }
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        [JsonProperty("idCalle")]
        public int IdCalle { get; set; }
        public Calle Calle { get; set; }
        [JsonProperty("numeroExterior")]
        public string NumeroExterior { get; set; }
        [JsonProperty("numeroInterior")]
        public string NumeroInterior { get; set; }
        [JsonProperty("codigoPostal")]
        public string CodigoPostal { get; set; }
        #endregion

    }
}