using Microsoft.EntityFrameworkCore;

namespace ControlEscolaApi.Model
{
    public class EstadoContext : DbContext
    {
        public EstadoContext(DbContextOptions<EstadoContext> options) : base(options) { }
        public DbSet<Estado> Estado { get; set; } = null!;
    }
}
