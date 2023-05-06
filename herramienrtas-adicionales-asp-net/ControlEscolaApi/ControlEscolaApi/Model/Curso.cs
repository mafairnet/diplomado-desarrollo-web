using System.ComponentModel.DataAnnotations.Schema;

namespace ControlEscolaApi.Model
{
    public class Curso
    {
        #region Atributos
        [Column("id")]
        public int ID { get; set; }
        [Column("nombre")]
        public string? Nombre { get; set; }
        [Column("id_perido")]
        public int? IdPeriod { get; set; }
        [Column("id_asignatura")]
        public int? IdAsignatura { get; set; }
        #endregion
    }
}
