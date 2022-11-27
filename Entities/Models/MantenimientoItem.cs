using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace SistemaMAV.Entities.Models;
public class MantenimientoItem {
    [Key]
    public int MantenimientoItemId { get; set; }

    [Required]
    public int MantenimientoId { get; set; }
    public Mantenimiento Mantenimiento { get; set; }

    [Required]
    public int PlanillaId { get; set; }
    public Planilla Planilla { get; set; }

    [Required]
    public int PlanillaItemId { get; set; }
    public PlanillaItem PlanillaItem { get; set; }

    [Column(TypeName = "bit")]
    public bool Reemplazo { get; set; }
}
