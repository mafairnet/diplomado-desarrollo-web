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
        public DbSet<CarreraEscuela> CarreraEscuela { get; set; }
        public DbSet<AsignaturaCarrera> AsignaturaCarrera { get; set; }
        public DbSet<UsuarioAsignatura> UsuarioAsignatura { get; set; }
        public DbSet<UsuarioCarrera> UsuarioCarrera{ get; set; }
        public DbSet<UsuarioCurso> UsuarioCurso{ get; set; }
        public DbSet<Calificacion> Calificacion { get; set; }
        public DbSet<CalificacionCursoUsuario> CalificacionCursoUsuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarreraEscuela>().HasKey(t => new { t.IdCarrera, t.IdEscuela});
            modelBuilder.Entity<AsignaturaCarrera>().HasKey(t => new { t.IdAsignatura, t.IdCarrera });
            modelBuilder.Entity<UsuarioAsignatura>().HasKey(t => new { t.IdUsuario, t.IdAsignatura });
            modelBuilder.Entity<UsuarioCarrera>().HasKey(t => new { t.IdUsuario, t.IdCarrera });
            modelBuilder.Entity<UsuarioCurso>().HasKey(t => new { t.IdUsuario, t.IdCurso });
            modelBuilder.Entity<CalificacionCursoUsuario>().HasKey(t => new { t.IdUsuario, t.IdCurso, t.IdCalificacion });

            base.OnModelCreating(modelBuilder);
        }
    }
}
