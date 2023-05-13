using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control_Escolar.Models
{
    public class UsuarioCarrera
    {
        #region Atributos
        [JsonProperty("idUsuario")]
        public int? IdUsuario { get; set; }
        [JsonProperty("idCarrera")]
        public int? IdCarrera { get; set; }
        
        #endregion
    }
}