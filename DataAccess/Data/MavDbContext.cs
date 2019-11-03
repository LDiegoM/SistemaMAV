using Microsoft.EntityFrameworkCore;

namespace SistemaMAV.DataAccess.Data {
    public class MavDbContext : DbContext {
        public MavDbContext (DbContextOptions<MavDbContext> options)
            : base(options)
        {
        }

        public DbSet<SistemaMAV.Entities.Models.Marca> Marca { get; set; }
    }
}