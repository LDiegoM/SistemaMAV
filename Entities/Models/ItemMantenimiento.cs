using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaMAV.Entities.Models;
public class ItemMantenimiento {
    [Key]
    public int ItemMantenimientoId { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    public string Detalle { get; set; }

    public int? KilometrosPredeterminado { get; set; }

    public int? TiempoPredeterminado { get; set; }

    public ICollection<PlanillaItem> PlanillaItems { get; set; }
    public ICollection<Stock> Stock { get; set; }
}
