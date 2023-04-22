using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control_Escolar.Models
{
    public class Carrera
    {
        #region Atributos
        public int? ID { get; set; }
        public string Nombre { get; set; }
        public Status Status { get; set; }
        #endregion
    }
}