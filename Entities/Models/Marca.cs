using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace SistemaMAV.Entities.Models;
public class Marca {
    [Key]
    public int MarcaId { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Detalle { get; set; }

    [Column(TypeName = "bit")]
    public bool Activo { get; set; }

    public ICollection<Modelo> Modelos { get; set; }
}
