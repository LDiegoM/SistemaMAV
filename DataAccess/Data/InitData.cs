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

                context.TipoUnidad.AddRange(
                    new TipoUnidad {
                        Detalle = "Automóvil",
                        Activo = true
                    },

                    new TipoUnidad {
                        Detalle = "Motocicleta",
                        Activo = true
                    },

                    new TipoUnidad {
                        Detalle = "Vehículo pesado",
                        Activo = true
                    }
                );

                context.ItemMantenimiento.AddRange(
                    new ItemMantenimiento {
                        Detalle = "Cambio de aceite",
                        KilometrosPredeterminado = 10000,
                        TiempoPredeterminado = 12
                    },

                    new ItemMantenimiento {
                        Detalle = "Correa de accesorios: sustituir",
                        KilometrosPredeterminado = 50000
                    }
                );

                context.SaveChanges();
            }
        }
    }
}