using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.ViewModels.Consultar;

public class TallerViewModel {
    [Display(Name = "Localidad")]
    public IEnumerable<SelectListItem> FiltroLocalidad { get; set; }

    [Display(Name = "Nombre")]
    public string? FiltroNombre { get; set; }

    public List<TallerItemConsulta> Talleres { get; set; }
    public int CantidadItems { get; set; }
    public bool MostrarResultados { get; set; }

    public TallerViewModel() {
        FiltroLocalidad = new List<SelectListItem>();

        Talleres = new List<TallerItemConsulta>();
        CantidadItems = 0;
        MostrarResultados = false;
    }
}

public class TallerItemConsulta {
    public int TallerId { get; set; }
    public string Nombre { get; set; }
    public Localidad? Localidad { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public int Puntaje { get; set; }

    public TallerItemConsulta() {
        Nombre = "";
        Direccion = "";
        Telefono = "";
        Email = "";
    }
}