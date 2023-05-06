using System.ComponentModel.DataAnnotations.Schema;

namespace ControlEscolaApi.Model
{
    public class Calle
    {
        #region Atributos
        [Column("id")]
        public int ID { get; set; }
        [Column("nombre")]
        public string? Nombre { get; set; }
        [Column("id_localidad")]
        public int? IdLocalidad { get; set; }
        #endregion
    }
}
