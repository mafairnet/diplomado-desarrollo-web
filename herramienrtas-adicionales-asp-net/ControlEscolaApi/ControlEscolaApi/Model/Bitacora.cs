using System.ComponentModel.DataAnnotations.Schema;

namespace ControlEscolaApi.Model
{
    public class Bitacora
    {
        #region Atributos
        [Column("id")]
        public int ID { get; set; }
        [Column("nombre")]
        public DateTime? TimeStamp { get; set; }
        [Column("id_accion")]
        public int? IdAccion { get; set; }
        [Column("id_usuario")]
        public int? IdUsuario { get; set; }
        [Column("data")]
        public String? Data { get; set; }
        #endregion
    }
}
