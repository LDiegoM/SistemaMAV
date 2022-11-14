using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaMAV.Entities.Models;
public class Modelo {
    [Key]
    public int ModeloId { get; set; }

    [Required]
    [Column(TypeName = "varchar(250)")]
    public string Detalle { get; set; }

    [Required]
    public int MarcaId { get; set; }
    public Marca Marca { get; set; }

    [Required]
    public int TipoUnidadId { get; set; }
    public TipoUnidad TipoUnidad { get; set; }

    [Required]
    public DateTime FechaAlta { get; set; }
    public DateTime? FechaBaja { get; set; }

    public ICollection<Planilla> Planillas { get; set; }
}
