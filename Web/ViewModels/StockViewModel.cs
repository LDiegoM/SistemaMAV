using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.ViewModels;
public class StockViewModel {

    [Display(Name = "Código")]
    public int StockId { get; set; }

    [Display(Name = "Item de mantenimiento")]
    [Required(ErrorMessage = "Debe ingresar el Item de mantenimiento")]
    public int ItemMantenimientoId { get; set; }
    public ItemMantenimiento? ItemMantenimiento { get; set; }

    [Display(Name = "Proveedor")]
    [Required(ErrorMessage = "Debe ingresar el Proveedor")]
    public int ProveedorId { get; set; }
    public Proveedor? Proveedor { get; set; }

    [Display(Name = "Cantidad en stock del proveedor")]
    public int CantidadEnStock { get; set; }

    [Display(Name = "Precio de venta")]
    public double PrecioVenta { get; set; }

    public StockViewModel() {}

    public StockViewModel(Stock stock) {
        StockId = stock.StockId;
        ItemMantenimientoId = stock.ItemMantenimientoId;
        ItemMantenimiento = stock.ItemMantenimiento;
        ProveedorId = stock.ProveedorId;
        Proveedor = stock.Proveedor;
        CantidadEnStock = stock.CantidadEnStock;
        PrecioVenta = stock.PrecioVenta;
    }

    public Stock ToStock() {
        return new Stock() {
            StockId = StockId,
            ItemMantenimientoId = ItemMantenimientoId,
            ProveedorId = ProveedorId,
            CantidadEnStock = CantidadEnStock,
            PrecioVenta = PrecioVenta
        };
    }
}
