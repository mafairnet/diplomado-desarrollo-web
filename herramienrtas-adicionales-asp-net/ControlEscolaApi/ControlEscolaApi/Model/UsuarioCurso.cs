using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlEscolaApi.Model
{
    
    public class UsuarioCurso
    {
        [Key, Column("id_usuario", Order = 0)]
        public int? IdUsuario { get; set; }
        [Key, Column("id_curso", Order = 1)]
        public int? IdCurso { get; set; }
        #region Atributos
        
        #endregion
    }
}
