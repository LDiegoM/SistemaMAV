using Microsoft.EntityFrameworkCore;

namespace SistemaMAV.UI.Web.Models {
    public class MavDbContext : DbContext {
        public MavDbContext (DbContextOptions<MavDbContext> options)
            : base(options)
        {
        }

        public DbSet<SistemaMAV.UI.Web.Models.Marca> Marca { get; set; }
    }
}