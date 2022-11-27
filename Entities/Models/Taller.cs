using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace SistemaMAV.Entities.Models;
public class Taller {
    [Key]
    public int TallerId { get; set; }

    [Required]
    [Column(TypeName = "varchar(250)")]
    public string Nombre { get; set; }

    public int AnioApertura { get; set; }

    [Column(TypeName = "varchar(300)")]
    public string Direccion { get; set; }

    [Required]
    public int LocalidadId { get; set; }
    public Localidad Localidad { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string Telefono { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string Email { get; set; }

    [Column(TypeName = "bit")]
    public bool Activo { get; set; }

    public int VotosPositivos { get; set; }

    public int VotosNegativos { get; set; }

    public ICollection<Mantenimiento> Mantenimientos { get; set; }
}
