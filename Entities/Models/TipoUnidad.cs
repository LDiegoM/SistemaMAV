using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaMAV.Entities.Models;
public class TipoUnidad {
    [Key]
    public int TipoUnidadId { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Detalle { get; set; }

    [Column(TypeName = "bit")]
    public bool Activo { get; set; }

    public ICollection<Modelo> Modelos { get; set; }
}
