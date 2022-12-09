using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.ViewModels;

public class PlanillaViewModel {

    [Display(Name = "Código")]
    public int PlanillaId { get; set; }

    [Display(Name = "Modelo")]
    [Required(ErrorMessage = "Debe ingresar el Modelo")]
    public int ModeloId { get; set; }
    [Display(Name = "Modelo")]
    public Modelo? Modelo { get; set; }        

    [Display(Name = "Detalle")]
    [Required(ErrorMessage = "Debe ingresar el Detalle de la Planilla")]
    public string Detalle { get; set; }

    [Display(Name = "Año Fabric.")]
    public int? AnioFabricacion { get; set; }

    [Display(Name = "Km para realizar mantenimiento")]
    public int? Kilometros { get; set; }

    [Display(Name = "Meses para realizar mantenimiento")]
    public int? Meses { get; set; }

    [Display(Name = "Versión")]
    public int? Version { get; set; }

    [Display(Name = "Activa?")]
    public bool Activo { get; set; }

    public ICollection<PlanillaItem>? PlanillaItems { get; set; }

    public PlanillaViewModel() {
        Detalle = "";
    }

    public PlanillaViewModel(Planilla planilla) {
        PlanillaId = planilla.PlanillaId;
        ModeloId = planilla.ModeloId;
        Modelo = planilla.Modelo;
        Detalle = planilla.Detalle;
        AnioFabricacion = planilla.AnioFabricacion;
        Kilometros = planilla.Kilometros;
        Meses = planilla.Meses;
        Version = planilla.Version;
        Activo = planilla.Activo;
    }

    public Planilla ToPlanilla() {
        return new Planilla() {
            PlanillaId = PlanillaId,
            ModeloId = ModeloId,
            Detalle = Detalle,
            AnioFabricacion = AnioFabricacion,
            Kilometros = Kilometros,
            Meses = Meses,
            Version = Version,
            Activo = Activo
        };
    }
}