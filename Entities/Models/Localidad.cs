using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace SistemaMAV.Entities.Models;
public class Localidad {
    [Key]
    public int LocalidadId { get; set; }

    [Required]
    [Column(TypeName = "varchar(250)")]
    public string Nombre { get; set; }

    public ICollection<Proveedor> Proveedores { get; set; }
    public ICollection<Taller> Talleres { get; set; }
}
