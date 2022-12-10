using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.ViewModels;
public class LocalidadViewModel {

    [Display(Name = "Código")]
    public int LocalidadId { get; set; }

    [Required(ErrorMessage = "Debe ingresar el Nombre")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; }

    public ICollection<Proveedor>? Proveedores { get; set; }
    public ICollection<Taller>? Talleres { get; set; }

    public LocalidadViewModel() {
        Nombre = "";
    }

    public LocalidadViewModel(Localidad localidad) {
        LocalidadId = localidad.LocalidadId;
        Nombre = localidad.Nombre;
    }

    public Localidad ToLocalidad() {
        return new Localidad() {
            LocalidadId = LocalidadId,
            Nombre = Nombre
        };
    }
}
