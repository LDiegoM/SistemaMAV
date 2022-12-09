using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.ViewModels;
public class MantenimientoViewModel {

    [Display(Name = "Código")]
    public int MantenimientoId { get; set; }

    [Display(Name = "Vehículo")]
    [Required(ErrorMessage = "Debe ingresar el Vehículo")]
    public int VehiculoId { get; set; }
    [Display(Name = "Vehiculo")]
    public Vehiculo? Vehiculo { get; set; }
    public string? VehiculoDetalle { get; set; }

    [Display(Name = "Taller")]
    [Required(ErrorMessage = "Debe ingresar el Taller")]
    public int TallerId { get; set; }
    [Display(Name = "Taller")]
    public Taller? Taller { get; set; }

    [Display(Name = "Planilla de mantenimiento")]
    [Required(ErrorMessage = "Debe ingresar la Planilla de mantenimiento")]
    public int PlanillaId { get; set; }
    [Display(Name = "Planilla de mantenimiento")]
    public Planilla? Planilla { get; set; }

    [Display(Name = "Fecha del mantenimiento")]
    [Required(ErrorMessage = "Debe ingresar la Fecha del mantenimiento")]
    public DateTime Fecha { get; set; }

    [Display(Name = "Kilómetros del vehículo")]
    public int Kilometros { get; set; }

    [Display(Name = "Precio total del mantenimiento")]
    public float Precio { get; set; }

    public ICollection<MantenimientoItem>? MantenimientoItems { get; set; }

    public MantenimientoViewModel() {}

    public MantenimientoViewModel(Mantenimiento mantenimiento) {
        MantenimientoId = mantenimiento.MantenimientoId;
        VehiculoId = mantenimiento.VehiculoId;
        Vehiculo = mantenimiento.Vehiculo;
        TallerId = mantenimiento.TallerId;
        Taller = mantenimiento.Taller;
        PlanillaId = mantenimiento.PlanillaId;
        Planilla = mantenimiento.Planilla;
        Fecha = mantenimiento.Fecha;
        Kilometros = mantenimiento.Kilometros;
        Precio = mantenimiento.Precio;
    }

    public Mantenimiento ToMantenimiento() {
        return new Mantenimiento() {
            MantenimientoId = MantenimientoId,
            VehiculoId = VehiculoId,
            TallerId = TallerId,
            PlanillaId = PlanillaId,
            Fecha = Fecha,
            Kilometros = Kilometros,
            Precio = Precio
        };
    }
}
