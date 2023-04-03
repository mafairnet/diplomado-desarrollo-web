using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalogo.Models
{
    public class Usuario
    {
        #region Atributos
        public int? ID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        #endregion

        }
}