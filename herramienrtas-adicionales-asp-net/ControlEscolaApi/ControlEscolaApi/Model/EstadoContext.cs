using Microsoft.EntityFrameworkCore;

namespace ControlEscolaApi.Model
{
    public class PaisContext : DbContext
    {
        public PaisContext(DbContextOptions<PaisContext> options) : base(options) { }
        public DbSet<Pais> Pais { get; set; } = null!;
    }
}
