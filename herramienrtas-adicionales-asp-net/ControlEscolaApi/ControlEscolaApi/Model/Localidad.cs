using System.ComponentModel.DataAnnotations.Schema;

namespace ControlEscolaApi.Model
{
    public class Localidad
    {
        #region Atributos
        [Column("id")]
        public int ID { get; set; }
        [Column("nombre")]
        public string? Nombre { get; set; }
        [Column("id_municipio")]
        public int? IdMunicipio { get; set; }
        #endregion
    }
}
