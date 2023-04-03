using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalogo.Dto
{
    public class DTOCalificacion
    {
        #region Atributos
        private int? id = null;
        private int? id_usuario = null;
        private int? calificacion = null;
        #endregion

        #region Get/Set
        public int? Id
        {
            get { return id; }
            set { id = value; }
        }

        public int? IdUsuario
        {
            get { return id_usuario; }
            set { id_usuario = value; }
        }

        public int? Calificacion
        {
            get { return calificacion; }
            set { calificacion = value; }
        }

        #endregion
    }
}