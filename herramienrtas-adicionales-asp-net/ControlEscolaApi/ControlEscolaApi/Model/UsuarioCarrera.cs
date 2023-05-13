using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlEscolaApi.Model
{
    
    public class UsuarioCarrera
    {
        [Key, Column("id_usuario", Order = 0)]
        public int? IdUsuario { get; set; }
        [Key, Column("id_carrera", Order = 1)]
        public int? IdCarrera { get; set; }
        #region Atributos
        
        #endregion
    }
}
