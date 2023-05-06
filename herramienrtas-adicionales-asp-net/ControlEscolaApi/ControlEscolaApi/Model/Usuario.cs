using System.ComponentModel.DataAnnotations.Schema;

namespace ControlEscolaApi.Model
{
    public class Usuario
    {
        #region Atributos
        [Column("id")]
        public int ID { get; set; }
        [Column("numero_identificacion")]
        public string? NumeroIdentificacion { get; set; }
        [Column("primer_nombre")]
        public string? PrimerNombre { get; set; }
        [Column("segundo_nombre")]
        public string? SegundoNombre { get; set; }
        [Column("primer_apellido")]
        public string? PrimerApellido { get; set; }
        [Column("segundo_apellido")]
        public string? SegundoApellido { get; set; }
        [Column("correo")]
        public string? Correo { get; set; }
        [Column("numero_fijo")]
        public long? NumeroFijo { get; set; }
        [Column("numero_movil")]
        public long? NumeroMovil { get; set; }
        [Column("id_ubicacion")]
        public int? IdUbicacion { get; set; }
        [Column("contrasena")]
        public string? Contrasena { get; set; }
        [Column("id_status")]
        public int? IdStatus { get; set; }

        #endregion
    }
}
