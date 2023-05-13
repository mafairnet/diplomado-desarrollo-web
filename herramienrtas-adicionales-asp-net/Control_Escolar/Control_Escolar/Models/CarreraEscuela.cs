using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control_Escolar.Models
{
    public class CarreraEscuela
    {
        #region Atributos
        [JsonProperty("idCarrera")]
        public int? IdCarrera { get; set; }
        [JsonProperty("idEscuela")]
        public int? IdEscuela { get; set; }
        #endregion
    }
}