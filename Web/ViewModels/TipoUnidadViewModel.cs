using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.ViewModels;

public class TipoUnidadViewModel {

    [Display(Name = "Código")]
    public int TipoUnidadId { get; set; }

    [Display(Name = "Detalle")]
    [Required(ErrorMessage = "Debe ingresar el Detalle")]
    public string Detalle { get; set; }
    
    [Display(Name = "Activa?")]
    public bool Activo { get; set; }

    public ICollection<Modelo>? Modelos { get; set; }

    public TipoUnidadViewModel() {}

    public TipoUnidadViewModel(TipoUnidad tipoUnidad) {
        TipoUnidadId = tipoUnidad.TipoUnidadId;
        Detalle = tipoUnidad.Detalle;
        Activo = tipoUnidad.Activo;
    }

    public TipoUnidad ToTipoUnidad() {
        return new TipoUnidad() {
            TipoUnidadId = TipoUnidadId,
            Detalle = Detalle,
            Activo = Activo
        };
    }
}