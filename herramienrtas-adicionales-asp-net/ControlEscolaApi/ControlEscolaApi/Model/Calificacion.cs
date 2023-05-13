using System.ComponentModel.DataAnnotations.Schema;

namespace ControlEscolaApi.Model
{
    public class Calificacion
    {
        #region Atributos
        [Column("id")]
        public int ID { get; set; }
        [Column("valor")]
        public string? Valor { get; set; }
        #endregion
    }
}
