using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.ViewModels;
public class PropietarioViewModel {

    [Display(Name = "Código")]
    public int PropietarioId { get; set; }

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

    [Display(Name = "Fecha de Alta")]
    [Required(ErrorMessage = "Debe ingresar la Fecha de Alta")]
    public DateTime FechaAlta { get; set; }

    [Display(Name = "Activ?")]
    public bool Activo { get; set; }

    public ICollection<Mantenimiento>? Mantenimientos { get; set; }

    public PropietarioViewModel() {
        UserId = "";
        Patente = "";
    }

    public PropietarioViewModel(Propietario propietario) {
        PropietarioId = propietario.PropietarioId;
        UserId = propietario.UserId;
        ModeloId = propietario.ModeloId;
        Modelo = propietario.Modelo;
        Patente = propietario.Patente;
        AnioFabricacion = propietario.AnioFabricacion;
        FechaAlta = propietario.FechaAlta;
        Activo = propietario.Activo;
    }

    public Propietario ToPropietario() {
        return new Propietario() {
            PropietarioId = PropietarioId,
            UserId = UserId,
            ModeloId = ModeloId,
            Patente = Patente,
            AnioFabricacion = AnioFabricacion,
            FechaAlta = FechaAlta,
            Activo = Activo
        };
    }
}
