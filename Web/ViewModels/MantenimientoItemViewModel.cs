using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace SistemaMAV.Entities.Models;
public class MantenimientoItemViewModel {

    [Display(Name = "Código")]
    public int MantenimientoItemId { get; set; }

    [Display(Name = "Mantenimiento")]
    [Required(ErrorMessage = "Debe ingresar ek Mantenimiento")]
    public int MantenimientoId { get; set; }
    [Display(Name = "Mantenimiento")]
    public Mantenimiento? Mantenimiento { get; set; }

    [Display(Name = "Planilla de mantenimiento")]
    [Required(ErrorMessage = "Debe ingresar la Planilla de mantenimiento")]
    public int PlanillaId { get; set; }
    [Display(Name = "Planilla de mantenimiento")]
    public Planilla? Planilla { get; set; }

    [Display(Name = "Item de la Planilla de Mantenimiento")]
    [Required(ErrorMessage = "Debe ingresar Item de la Planilla de mantenimiento")]
    public int PlanillaItemId { get; set; }
    [Display(Name = "Item de Planilla de Mantenimiento")]
    public PlanillaItem? PlanillaItem { get; set; }

    [Display(Name = "Fue reemplazado?")]
    public bool Reemplazo { get; set; }

    public MantenimientoItemViewModel() {}

    public MantenimientoItemViewModel(MantenimientoItem mantenimientoItem) {
        MantenimientoItemId = mantenimientoItem.MantenimientoItemId;
        MantenimientoId = mantenimientoItem.MantenimientoId;
        Mantenimiento = mantenimientoItem.Mantenimiento;
        PlanillaId = mantenimientoItem.PlanillaId;
        Planilla = mantenimientoItem.Planilla;
        PlanillaItemId = mantenimientoItem.PlanillaItemId;
        PlanillaItem = mantenimientoItem.PlanillaItem;
        Reemplazo = mantenimientoItem.Reemplazo;
    }

    public MantenimientoItem ToMantenimientoItem() {
        return new MantenimientoItem() {
            MantenimientoItemId = MantenimientoItemId,
            MantenimientoId = MantenimientoId,
            PlanillaId = PlanillaId,
            PlanillaItemId = PlanillaItemId,
            Reemplazo = Reemplazo
        };
    }
}
