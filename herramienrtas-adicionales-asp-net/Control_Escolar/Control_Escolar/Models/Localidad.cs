using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control_Escolar.Models
{
    public class Localidad
    {
        #region Atributos
        public int? ID { get; set; }
        public string Nombre { get; set; }
        public Municipio Municipio { get; set; }
        #endregion
        
    }
}