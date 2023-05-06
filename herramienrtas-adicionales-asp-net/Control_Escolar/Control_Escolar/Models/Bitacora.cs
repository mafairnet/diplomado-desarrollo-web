using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control_Escolar.Models
{
    public class Bitacora
    {
        #region Atributos
        [JsonProperty("id")]
        public int? ID { get; set; }
        [JsonProperty("timestamp")]
        public DateTime TimeStamp { get; set; }
        [JsonProperty("idAccion")]
        public int IdAccion { get; set; }
        [JsonProperty("idUsuario")]
        public int IdUsuario { get; set; }
        [JsonProperty("data")]
        public String Data { get; set; }
        public Accion Accion { get; set; }
        public Usuario Usuario { get; set; }
        #endregion

    }
}