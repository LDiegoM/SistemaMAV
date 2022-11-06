using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.ViewModels;

public class PlanillaItemViewModel {

    [Display(Name = "Cód. Item")]
    public int PlanillaItemId { get; set; }

    [Display(Name = "Planilla")]
    [Required(ErrorMessage = "Debe ingresar la Planilla")]
    public int PlanillaId { get; set; }
    [Display(Name = "Planilla")]
    public Planilla? Planilla { get; set; }

    [Display(Name = "Item de Mantenimiento")]
    [Required(ErrorMessage = "Debe ingresar el Item de Mantenimiento")]
    public int ItemMantenimientoId { get; set; }
    [Display(Name = "Item de Mantenimiento")]
    public ItemMantenimiento? ItemMantenimiento { get; set; }

    [Display(Name = "Kilómetros")]
    [DisplayFormat(NullDisplayText = ".")]
    public int? Kilometros { get; set; }

    [Display(Name = "Meses")]
    [DisplayFormat(NullDisplayText = ".")]
    public int? Meses { get; set; }

    [Display(Name = "Recomendaciones")]
    [DisplayFormat(NullDisplayText = ".")]
    public string? Recomendaciones { get; set; }

    [Display(Name = "Observaciones")]
    [DisplayFormat(NullDisplayText = ".")]
    public string? Observaciones { get; set; }

    [Display(Name = "Info. Extra")]
    [DisplayFormat(NullDisplayText = ".")]
    public string? InfoExtra { get; set; }

    public PlanillaItemViewModel() {}

    public PlanillaItemViewModel(PlanillaItem planillaItem) {
        PlanillaItemId = planillaItem.PlanillaItemId;
        PlanillaId = planillaItem.PlanillaId;
        Planilla = planillaItem.Planilla;
        ItemMantenimientoId = planillaItem.ItemMantenimientoId;
        ItemMantenimiento = planillaItem.ItemMantenimiento;
        Kilometros = planillaItem.Kilometros;
        Meses = planillaItem.Meses;
        Recomendaciones = planillaItem.Recomendaciones;
        Observaciones = planillaItem.Observaciones;
        InfoExtra = planillaItem.InfoExtra;
    }

    public PlanillaItem ToPlanillaItem() {
        return new PlanillaItem() {
            PlanillaItemId = PlanillaItemId,
            PlanillaId = PlanillaId,
            ItemMantenimientoId = ItemMantenimientoId,
            Kilometros = Kilometros,
            Meses = Meses,
            Recomendaciones = Recomendaciones,
            Observaciones = Observaciones,
            InfoExtra = InfoExtra
        };
    }
}