﻿using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "admin", Name = "Administrador", NormalizedName = "ADMINISTRADOR"}
        );
        builder.Entity<IdentityUser>().HasData(
            new IdentityUser {
                Id = "admin",
                UserName = "Administrador",
                NormalizedUserName = "ADMINISTRADOR",
                Email = "admin@mav.com",
                NormalizedEmail = "ADMIN@MAV.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEH7g5Hh0bEetkum/buwj0Y2cRVFHWuUe4UZec+vEugilfLt8KnFaPvpVDlvy8X05iw==",
                SecurityStamp = "OYWWGEEW7PAULQNQAM4DL5FERLEQLEPP",
                ConcurrencyStamp = "9250d0a9-2184-4ce1-80ac-73a6d3d811de",
                LockoutEnabled = true
            },
            new IdentityUser {
                Id = "prop",
                UserName = "Propietario",
                NormalizedUserName = "PROPIETARIO",
                Email = "propietario@mav.com",
                NormalizedEmail = "PROPIETARIO@MAV.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEKnMEx8PAlltiKgEBDYvgoBQAG/ea2AdmcsjU7sf8tb/gq+tI064hbMkuhXs59a+Jw==",
                SecurityStamp = "EQXVLBMZVTIJMWVT6EDOUWZ3YFXA5FIP",
                ConcurrencyStamp = "e7513c25-eb2e-4101-9480-57c00b84d1e2",
                LockoutEnabled = true
            }
        );
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = "admin", RoleId = "admin" }
        );

        builder.Entity<Marca>().HasData(
            new Marca { MarcaId = 1, Detalle = "Ford", Activo = true },
            new Marca { MarcaId = 2, Detalle = "Chevrolet", Activo = true }
        );

        builder.Entity<TipoUnidad>().HasData(
            new TipoUnidad { TipoUnidadId = 1, Detalle = "Automóvil", Activo = true },
            new TipoUnidad { TipoUnidadId = 2, Detalle = "Motocicleta", Activo = true },
            new TipoUnidad { TipoUnidadId = 3, Detalle = "Vehículo pesado", Activo = true }
        );

        builder.Entity<Modelo>().HasData(
            new Modelo { ModeloId = 1, Detalle = "Chevrolet Agile", FechaAlta = new DateTime(2005, 1, 1), MarcaId = 2, TipoUnidadId = 1 },
            new Modelo { ModeloId = 2, Detalle = "Ford Fiesta Kinetic Design", FechaAlta = new DateTime(2009, 1, 1), MarcaId = 1, TipoUnidadId = 1 }
        );

        builder.Entity<ItemMantenimiento>().HasData(
            new ItemMantenimiento { ItemMantenimientoId = 1, Detalle = "Cambio de aceite", KilometrosPredeterminado = 10000, TiempoPredeterminado = 12 },
            new ItemMantenimiento { ItemMantenimientoId = 2, Detalle = "Verificar presión de neumáticos", KilometrosPredeterminado = 2000, TiempoPredeterminado = 2 },
            new ItemMantenimiento { ItemMantenimientoId = 3, Detalle = "Correa de accesorios: sustituir", KilometrosPredeterminado = 50000 },
            new ItemMantenimiento { ItemMantenimientoId = 4, Detalle = "Control de fugas de fluidos" },
            new ItemMantenimiento { ItemMantenimientoId = 5, Detalle = "Prueba en ruta" },
            new ItemMantenimiento { ItemMantenimientoId = 6, Detalle = "Sustituir bujías", KilometrosPredeterminado = 30000 },
            new ItemMantenimiento { ItemMantenimientoId = 7, Detalle = "Correa de distribución: inspeccionar" },
            new ItemMantenimiento { ItemMantenimientoId = 8, Detalle = "Correa de distribución: sustituir", KilometrosPredeterminado = 50000 },
            new ItemMantenimiento { ItemMantenimientoId = 9, Detalle = "Aceite de transmisión" },
            new ItemMantenimiento { ItemMantenimientoId = 10, Detalle = "Líquido de frenos" },
            new ItemMantenimiento { ItemMantenimientoId = 11, Detalle = "Fluido de la dirección asistida" },
            new ItemMantenimiento { ItemMantenimientoId = 12, Detalle = "Pedal de embrague" },
            new ItemMantenimiento { ItemMantenimientoId = 13, Detalle = "Filtro de aire: inspeccionar" },
            new ItemMantenimiento { ItemMantenimientoId = 14, Detalle = "Filtro de aire: sustituir" },
            new ItemMantenimiento { ItemMantenimientoId = 15, Detalle = "Filtro de combustible: sustituir" },
            new ItemMantenimiento { ItemMantenimientoId = 16, Detalle = "Prefiltro de bomba de combustible" },
            new ItemMantenimiento { ItemMantenimientoId = 17, Detalle = "Sistema de aire acondicionado" },
            new ItemMantenimiento { ItemMantenimientoId = 18, Detalle = "Sistema de refrigeración de motor" }
        );

        builder.Entity<Planilla>().HasData(
            new Planilla { PlanillaId = 1, ModeloId = 1, Detalle = "Chevrolet Agile", AnioFabricacion = 2015, Version = 1, Activo = true }
        );

        builder.Entity<PlanillaItem>().HasData(
            new PlanillaItem { PlanillaItemId = 1, PlanillaId = 1, ItemMantenimientoId = 1, Kilometros = 10000, Meses = 12, Recomendaciones = "Utilizar aceites Elaion F50 d1 (Dexos 1 API-SN ILSAC GF-5, grado SAE 5W30).", Observaciones = "Cambiar y verificar nivel con el motor a temperatura de operación normal.", InfoExtra = "Cambie el aceite del motor y el filtro de aceite conforme a los intervalos de tiempo o kilómetros recorridos, ya que los mismos pierden sus propiedades de lubricación no solo debido al funcionamiento del motor, sino también a su envejecimiento. Verificar el nivel de aceite semanalmente o antes de iniciar un viaje de más de 50 kilómetros. Tener en cuenta que el gasto promedio de aceite es de 0,8 litros cada 1000 km." },
            new PlanillaItem { PlanillaItemId = 2, PlanillaId = 1, ItemMantenimientoId = 2, Kilometros = 2000, Meses = 2, Recomendaciones = "No esperar al siguiente servicio. Verificar frecuentemente en estaciones de servicio o talleres especializados en neumáticos." },
            new PlanillaItem { PlanillaItemId = 3, PlanillaId = 1, ItemMantenimientoId = 3, Kilometros = 50000, InfoExtra = "Verificar el estado de los tensores." },
            new PlanillaItem { PlanillaItemId = 4, PlanillaId = 1, ItemMantenimientoId = 4, Kilometros = 10000, Meses = 12, InfoExtra = "Inspeccionar fugas de aceite, líquido refrigerante, de dirección, de freno, grasa de la caja de cambios y líquido lava-parabrisas." },
            new PlanillaItem { PlanillaItemId = 5, PlanillaId = 1, ItemMantenimientoId = 5, Kilometros = 30000, InfoExtra = "Inspeccionar si el vehículo presenta anomalías ocasionales. Realizar una prueba en ruta después de la inspección." },
            new PlanillaItem { PlanillaItemId = 6, PlanillaId = 1, ItemMantenimientoId = 6, Kilometros = 30000 },
            new PlanillaItem { PlanillaItemId = 7, PlanillaId = 1, ItemMantenimientoId = 7, Observaciones = "Primer control a los 20.000 Km, luego cada 50.000 Km.", InfoExtra = "Inspeccionar el estado de la correa y del tensor automático." },
            new PlanillaItem { PlanillaItemId = 8, PlanillaId = 1, ItemMantenimientoId = 8, Kilometros = 50000 },
            new PlanillaItem { PlanillaItemId = 9, PlanillaId = 1, ItemMantenimientoId = 9, Kilometros = 10000, Meses = 12, Recomendaciones = "Aceite mineral para cajas de cambios, SAE 75W85, engranajes helicoidales, color rojo.", Observaciones = "Caja de velocidades", InfoExtra = "Verificar el nivel y sustituir si fuera necesario." },
            new PlanillaItem { PlanillaItemId = 10, PlanillaId = 1, ItemMantenimientoId = 10, Kilometros = 20000, Meses = 24, Recomendaciones = "Líquido de frenos DOT 4 de ACDelco.", InfoExtra = "Verificar el nivel y completar al nivel si hay fuga. Se debe corregir inmediatamente si hay fuga." },
            new PlanillaItem { PlanillaItemId = 11, PlanillaId = 1, ItemMantenimientoId = 11, Kilometros = 10000, Recomendaciones = "Aceite Dexron II de ACDelco.", InfoExtra = "Verificar el nivel. No requiere cambio, excepto baja del nivel." },
            new PlanillaItem { PlanillaItemId = 12, PlanillaId = 1, ItemMantenimientoId = 12, Kilometros = 30000, InfoExtra = "Comprobar el recorrido." },
            new PlanillaItem { PlanillaItemId = 13, PlanillaId = 1, ItemMantenimientoId = 13, Observaciones = "Primer control a los 20.000 Km, luego cada 30.000 Km.", InfoExtra = "Limpiar el filtro si fuera necesario." },
            new PlanillaItem { PlanillaItemId = 14, PlanillaId = 1, ItemMantenimientoId = 14, Kilometros = 30000 },
            new PlanillaItem { PlanillaItemId = 15, PlanillaId = 1, ItemMantenimientoId = 15, Kilometros = 20000 },
            new PlanillaItem { PlanillaItemId = 16, PlanillaId = 1, ItemMantenimientoId = 16, Kilometros = 80000, InfoExtra = "Colador de la bomba de combustible." },
            new PlanillaItem { PlanillaItemId = 17, PlanillaId = 1, ItemMantenimientoId = 17, Kilometros = 10000, Recomendaciones = "Gas R134a", InfoExtra = "Controlar en cada inspección. No requiere sustitución, excepto que haya fuga." },
            new PlanillaItem { PlanillaItemId = 18, PlanillaId = 1, ItemMantenimientoId = 18, Recomendaciones = "Inspeccionar el nivel de líquido refrigerante mensualmente.", InfoExtra = "Cambiar el líquido refrigerante y reparar posibles fugas. Antes de cambiar se recomienda limpiar el sistema de refrigeración." }
        );
    }

    public DbSet<Marca>? Marca { get; set; }
    public DbSet<TipoUnidad>? TipoUnidad { get; set; }
    public DbSet<Modelo>? Modelo { get; set; }
    public DbSet<ItemMantenimiento>? ItemMantenimiento { get; set; }
    public DbSet<Planilla>? Planilla { get; set; }
    public DbSet<PlanillaItem>? PlanillaItem { get; set; }
}