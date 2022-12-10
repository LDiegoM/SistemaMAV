using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.ViewModels;
public class VehiculoViewModel {

    [Display(Name = "Código")]
    public int VehiculoId { get; set; }

    [Display(Name = "Usuario")]
    [Required(ErrorMessage = "Debe ingresar el Usuario")]
    public string UserId { get; set; }

    [Display(Name = "Modelo")]
    [Required(ErrorMessage = "Debe ingresar el Modelo")]
    public int ModeloId { get; set; }
    [Display(Name = "Modelo")]
    public Modelo? Modelo { get; set; }

    [Required(ErrorMessage = "Debe ingresar la Patente")]
    [Display(Name = "Patente")]
    public string Patente { get; set; }

    [Display(Name = "Año Fabric.")]
    public int AnioFabricacion { get; set; }

    [Display(Name = "Kilómetros")]
    public int Kilometros { get; set; }

    [Display(Name = "Fecha de Alta")]
    [Required(ErrorMessage = "Debe ingresar la Fecha de Alta")]
    public DateTime FechaAlta { get; set; }

    [Display(Name = "Activo?")]
    public bool Activo { get; set; }

    [Display(Name = "Próx. Mantenimiento (km)")]
    public string? ProximoMantenimiento { get; set; }

    public Planilla? Planilla { get; set; }

    public ICollection<Mantenimiento>? Mantenimientos { get; set; }

    public VehiculoViewModel() {
        UserId = "";
        Patente = "";
    }

    public VehiculoViewModel(Vehiculo vehiculo) {
        VehiculoId = vehiculo.VehiculoId;
        UserId = vehiculo.UserId;
        ModeloId = vehiculo.ModeloId;
        Modelo = vehiculo.Modelo;
        Patente = vehiculo.Patente;
        AnioFabricacion = vehiculo.AnioFabricacion;
        Kilometros = vehiculo.Kilometros;
        FechaAlta = vehiculo.FechaAlta;
        Activo = vehiculo.Activo;
    }

    public Vehiculo ToVehiculo() {
        return new Vehiculo() {
            VehiculoId = VehiculoId,
            UserId = UserId,
            ModeloId = ModeloId,
            Patente = Patente,
            AnioFabricacion = AnioFabricacion,
            Kilometros = Kilometros,
            FechaAlta = FechaAlta,
            Activo = Activo
        };
    }
}
