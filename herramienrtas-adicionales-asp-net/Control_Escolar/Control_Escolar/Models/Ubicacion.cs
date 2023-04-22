using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control_Escolar.Models
{
    public class Ubicacion
    {
        #region Atributos
        public int? ID { get; set; }
        public string Nombre { get; set; }
        public Calle Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string CodigoPostal { get; set; }
        #endregion

    }
}