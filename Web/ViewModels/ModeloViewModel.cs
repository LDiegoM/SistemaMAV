using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.ViewModels;

public class ModeloViewModel {

    [Display(Name = "Código")]
    public int ModeloId { get; set; }

    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "Debe ingresar el Nombre del Modelo")]
    public string Detalle { get; set; }

    [Display(Name = "Marca")]
    [Required(ErrorMessage = "Debe ingresar la Marca")]
    public int MarcaId { get; set; }
    [Display(Name = "Marca")]
    public Marca? Marca { get; set; }        

    [Display(Name = "Tipo de Unidad")]
    [Required(ErrorMessage = "Debe ingresar el Tipo de Unidad")]
    public int TipoUnidadId { get; set; }
    [Display(Name = "Tipo de Unidad")]
    public TipoUnidad? TipoUnidad { get; set; }

    [Display(Name = "Fecha de Alta")]
    [Required(ErrorMessage = "Debe ingresar la Fecha de Alta")]
    public DateTime FechaAlta { get; set; }

    [Display(Name = "Fecha de Baja")]
    public DateTime? FechaBaja { get; set; }

    public ICollection<Planilla>? Planillas { get; set; }
    public ICollection<Propietario>? Propietarios { get; set; }

    public ModeloViewModel() {
        Detalle = "";
    }

    public ModeloViewModel(Modelo modelo) {
        ModeloId = modelo.ModeloId;
        Detalle = modelo.Detalle;
        MarcaId = modelo.MarcaId;
        Marca = modelo.Marca;
        TipoUnidadId = modelo.TipoUnidadId;
        TipoUnidad = modelo.TipoUnidad;
        FechaAlta = modelo.FechaAlta;
        FechaBaja = modelo.FechaBaja;
    }

    public Modelo ToModelo() {
        return new Modelo() {
            ModeloId = ModeloId,
            Detalle = Detalle,
            MarcaId = MarcaId,
            TipoUnidadId = TipoUnidadId,
            FechaAlta = FechaAlta,
            FechaBaja = FechaBaja
        };
    }
}