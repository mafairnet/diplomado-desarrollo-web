using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlEscolaApi.Model
{
    
    public class UsuarioAsignatura
    {
        [Key, Column("id_usuario", Order = 0)]
        public int? IdUsuario { get; set; }
        [Key, Column("id_asignatura", Order = 1)]
        public int? IdAsignatura { get; set; }
        #region Atributos
        
        #endregion
    }
}
