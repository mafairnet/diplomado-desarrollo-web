using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Control_Escolar.Models
{
    public class Usuario
    {
        #region Atributos
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("numeroIdentificacion")]
        public string NumeroIdentificacion { get; set; }

        [JsonProperty("primerNombre")]
        public string PrimerNombre { get; set; }

        [JsonProperty("segundoNombre")]
        public string SegundoNombre { get; set; }

        [JsonProperty("primerApellido")]
        public string PrimerApellido { get; set; }

        [JsonProperty("segundoApellido")]
        public string SegundoApellido { get; set; }

        [JsonProperty("correo")]
        public string Correo { get; set; }

        [JsonProperty("numeroFijo")]
        public long NumeroFijo { get; set; }

        [JsonProperty("numeroMovil")]
        public long NumeroMovil { get; set; }

        [JsonProperty("idUbicacion")]
        public int IdUbicacion { get; set; }

        [JsonProperty("contrasena")]
        public string Contrasena { get; set; }

        [JsonProperty("idStatus")]
        public int IdStatus { get; set; }
        public Status Status { get; set; }
        public Ubicacion Ubicacion { get; set; }
        public List<Asignatura> Asignaturas {get; set;}
        public List<Carrera> Carreras { get; set; }
        public List<Curso> Cursos { get; set; }
        public Calificacion CalificacionCurso { get; set; } 
        #endregion
    }
}