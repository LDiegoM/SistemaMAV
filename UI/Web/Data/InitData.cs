using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace SistemaMAV.UI.Web.Models {
    public static class InitData {
        public static void Initialize(IServiceProvider serviceProvider) {
            using (var context = new MavDbContext(serviceProvider.GetRequiredService<DbContextOptions<MavDbContext>>()))
            {
                if (context.Marca.Any()) {
                    return;   // DB has been seeded
                }

                context.Marca.AddRange(
                    new Marca {
                        MarcaId = 1,
                        Detalle = "Ford",
                        Activo = true
                    },

                    new Marca {
                        MarcaId = 2,
                        Detalle = "Chevrolet",
                        Activo = true
                    }
                );
                context.SaveChanges();
            }
        }
    }
}