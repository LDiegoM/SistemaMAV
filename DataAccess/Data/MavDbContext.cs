using Microsoft.EntityFrameworkCore;

namespace SistemaMAV.DataAccess.Data {
    public class MavDbContext : DbContext {
        public MavDbContext (DbContextOptions<MavDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<SistemaMAV.Entities.Models.Marca> Marca { get; set; }
    }
}