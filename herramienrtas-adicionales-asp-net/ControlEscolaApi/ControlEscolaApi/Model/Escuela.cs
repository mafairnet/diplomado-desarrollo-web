using System.ComponentModel.DataAnnotations.Schema;

namespace ControlEscolaApi.Model
{
    public class Escuela
    {
        #region Atributos
        [Column("id")]
        public int ID { get; set; }
        [Column("nombre")]
        public string? Nombre { get; set; }
        [Column("id_ubicacion")]
        public int? IdUbicacion { get; set; }
        [Column("id_status")]
        public int? IdStatus { get; set; }
        #endregion
    }
}
