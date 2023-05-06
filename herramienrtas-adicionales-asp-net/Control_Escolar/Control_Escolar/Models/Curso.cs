using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control_Escolar.Models
{
    public class Curso
    {
        #region Atributos
        [JsonProperty("id")]
        public int? ID { get; set; }
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        [JsonProperty("idPeriodo")]
        public int IdPeriodo { get; set; }
        [JsonProperty("idAsignatura")]
        public int IdAsignatura { get; set; }
        public Periodo Periodo { get; set; }
        public Asignatura Asignatura { get; set; }
        #endregion

    }
}