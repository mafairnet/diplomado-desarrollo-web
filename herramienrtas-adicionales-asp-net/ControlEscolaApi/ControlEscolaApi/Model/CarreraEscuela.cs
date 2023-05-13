using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlEscolaApi.Model
{
    
    public class CarreraEscuela
    {
        #region Atributos
        [Key, Column("id_carrera", Order = 0)]
        public int? IdCarrera { get; set; }
        [Key, Column("id_escuela",Order = 1)]
        public int? IdEscuela { get; set; }
        #endregion
    }
}
