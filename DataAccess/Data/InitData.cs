using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.DataAccess.Data {
    public static class InitData {
        public static void Initialize(IServiceProvider serviceProvider) {
            using (var context = new MavDbContext(serviceProvider.GetRequiredService<DbContextOptions<MavDbContext>>()))
            {
                if (context.Marca.Any()) {
                    return;   // DB has been seeded
                }

                context.Marca.AddRange(
                    new Marca {
                        Detalle = "Ford",
                        Activo = true
                    },

                    new Marca {
                        Detalle = "Chevrolet",
                        Activo = true
                    }
                );
                context.SaveChanges();
            }
        }
    }
}