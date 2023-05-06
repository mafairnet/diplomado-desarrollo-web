using System.ComponentModel.DataAnnotations.Schema;

namespace ControlEscolaApi.Model
{
    public class Ubicacion
    {
        #region Atributos
        [Column("id")]
        public int ID { get; set; }
        [Column("nombre")]
        public string? Nombre { get; set; }
        [Column("id_calle")]
        public int? IdCalle { get; set; }
        [Column("numero_exterior")]
        public string? NumeroExterior { get; set; }
        [Column("numero_interior")]
        public string? NumeroInterior { get; set; }
        [Column("codigo_postal")]
        public string? CodigoPostal { get; set; }
        #endregion
    }
}
