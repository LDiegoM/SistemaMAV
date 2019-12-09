using Microsoft.EntityFrameworkCore;

namespace SistemaMAV.DataAccess.Data {
    public class MavDbContext : DbContext {
        public MavDbContext (DbContextOptions<MavDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<SistemaMAV.Entities.Models.Marca> Marca { get; set; }
        public DbSet<SistemaMAV.Entities.Models.TipoUnidad> TipoUnidad { get; set; }
        public DbSet<SistemaMAV.Entities.Models.Modelo> Modelo { get; set; }
        public DbSet<SistemaMAV.Entities.Models.ItemMantenimiento> ItemMantenimiento { get; set; }
        public DbSet<SistemaMAV.Entities.Models.Planilla> Planilla { get; set; }
        public DbSet<SistemaMAV.Entities.Models.PlanillaItem> PlanillaItem { get; set; }
    }
}