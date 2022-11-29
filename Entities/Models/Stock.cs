using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace SistemaMAV.Entities.Models;
public class Stock {
    [Key]
    public int StockId { get; set; }

    [Required]
    public int ItemMantenimientoId { get; set; }
    public ItemMantenimiento ItemMantenimiento { get; set; }

    [Required]
    public int ProveedorId { get; set; }
    public Proveedor Proveedor { get; set; }

    [Required]
    [Column(TypeName = "varchar(250)")]
    public string Detalle { get; set; }

    public int CantidadEnStock { get; set; }

    public double PrecioVenta { get; set; }
}
