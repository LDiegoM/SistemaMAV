using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.ViewModels;

public class MarcaViewModel {

    [Display(Name = "Código")]
    public int MarcaId { get; set; }

    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "Debe ingresar el Nombre")]
    public string Detalle { get; set; }

    [Display(Name = "Activa?")]
    public bool Activo { get; set; }

    public ICollection<Modelo>? Modelos { get; set; }

    public MarcaViewModel() {
        Detalle = "";
     }

    public MarcaViewModel(Marca marca) {
        MarcaId = marca.MarcaId;
        Detalle = marca.Detalle;
        Activo = marca.Activo;
    }

    public Marca ToMarca() {
        return new Marca() {
            MarcaId = MarcaId,
            Detalle = Detalle,
            Activo = Activo
        };
    }
}
