using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaMAV.Entities.Models;
public class Planilla {
    [Key]
    public int PlanillaId { get; set; }

    [Required]
    public int ModeloId { get; set; }
    public Modelo Modelo { get; set; }

    [Required]
    [Column(TypeName = "varchar(270)")]
    public string Detalle { get; set; }

    public int? AnioFabricacion { get; set; }

    [DefaultValue(1)]
    public int? Version { get; set; }

    [Column(TypeName = "bit")]
    public bool Activo { get; set; }

    public ICollection<PlanillaItem> PlanillaItems { get; set; }
}
