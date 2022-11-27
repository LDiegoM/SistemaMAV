using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace SistemaMAV.Entities.Models;
public class Mantenimiento {
    [Key]
    public int MantenimientoId { get; set; }

    [Required]
    public int PropietarioId { get; set; }
    public Propietario Propietario { get; set; }

    [Required]
    public int TallerId { get; set; }
    public Taller Taller { get; set; }

    [Required]
    public int PlanillaId { get; set; }
    public Planilla Planilla { get; set; }

    [Required]
    public DateTime Fecha { get; set; }

    public int Kilometros { get; set; }

    public float Precio { get; set; }

    public ICollection<MantenimientoItem> MantenimientoItems { get; set; }
}
