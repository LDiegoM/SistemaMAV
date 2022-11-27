using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace SistemaMAV.Entities.Models;
public class Propietario {
    [Key]
    public int PropietarioId { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string UserId { get; set; }

    [Required]
    public int ModeloId { get; set; }
    public Modelo Modelo { get; set; }

    public string Patente { get; set; }

    [Required]
    public int AnioFabricacion { get; set; }

    [Required]
    public DateTime FechaAlta { get; set; }

    [Column(TypeName = "bit")]
    public bool Activo { get; set; }

    public ICollection<Mantenimiento> Mantenimientos { get; set; }
}
