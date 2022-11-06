using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaMAV.Entities.Models;
public class PlanillaItem {
    [Key]
    public int PlanillaItemId { get; set; }

    [Required]
    public int PlanillaId { get; set; }
    public Planilla Planilla { get; set; }

    [Required]
    public int ItemMantenimientoId { get; set; }
    public ItemMantenimiento ItemMantenimiento { get; set; }

    public int? Kilometros { get; set; }
    public int? Meses { get; set; }
    public string? Recomendaciones { get; set; }
    public string? Observaciones { get; set; }
    public string? InfoExtra { get; set; }
}
