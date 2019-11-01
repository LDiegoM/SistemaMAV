using Microsoft.EntityFrameworkCore;

namespace SistemaMAV.Models {
    public class MavDbContext : DbContext {
        public MavDbContext (DbContextOptions<MavDbContext> options)
            : base(options)
        {
        }

        public DbSet<SistemaMAV.Models.Marca> Marca { get; set; }
    }
}