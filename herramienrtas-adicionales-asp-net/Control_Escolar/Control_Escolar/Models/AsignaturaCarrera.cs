using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control_Escolar.Models
{
    public class AsignaturaCarrera
    {
        #region Atributos
        public Asignatura Asignatura { get; set; }
        public Carrera Carrera { get; set; }
        #endregion
    }
}