using Microsoft.EntityFrameworkCore;

namespace ControlEscolaApi.Model
{
    public class CatalogoEscolarContext : DbContext
    {
        public CatalogoEscolarContext(DbContextOptions<CatalogoEscolarContext> options) : base(options) { }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<Localidad> Localidad { get; set; }
        public DbSet<Calle> Calle { get; set; }
        public DbSet<Ubicacion> Ubicacion { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Periodo> Periodo { get; set; }
        public DbSet<Escuela> Escuela { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Accion> Accion { get; set; }
        public DbSet<Bitacora> Bitacora { get; set; }
        public DbSet<Asignatura> Asignatura { get; set; }
        public DbSet<Carrera> Carrera { get; set; }
    }
}
