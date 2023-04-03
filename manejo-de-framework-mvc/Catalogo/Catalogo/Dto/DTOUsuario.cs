using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalogo.Dto
{
    public class DTOUsuario
    {
        #region Atributos
        private int? id = null;
        private string first_name = null;
        private string second_name = null;
        private string username = null;
        private string password = null;
        #endregion

        #region Get/Set
        public int? Id {
            get { return id; }
            set { id = value; }
        }

        public string FirstName {
            get { return first_name; }
            set { first_name = value; }
        }

        public string SecondName{
            get { return second_name; }
            set { second_name = value; }
        }

        public string Password{
            get { return password; }
            set { password = value; }
        }

        public string UserName{
            get { return username; }
            set { username = value; }
        }
        #endregion
    }
}