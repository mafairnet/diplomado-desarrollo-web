using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control_Escolar.Models
{
    public class UsuarioAsignatura
    {
        [JsonProperty("idUsuario")]
        public int? IdUsuario { get; set; }
        #region Atributos
        [JsonProperty("idAsignatura")]
        public int? IdAsignatura { get; set; }
        #endregion
    }
}