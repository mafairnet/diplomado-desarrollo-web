using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlEscolaApi.Model
{
    
    public class AsignaturaCarrera
    {
        [Key, Column("id_asignatura", Order = 0)]
        public int? IdAsignatura { get; set; }
        #region Atributos
        [Key, Column("id_carrera", Order = 1)]
        public int? IdCarrera { get; set; }
        #endregion
    }
}
