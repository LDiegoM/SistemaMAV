using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.ViewModels.Consultar;

public class IndexViewModel {
    [Display(Name = "Marca")]
    public IEnumerable<SelectListItem> FiltroMarca { get; set; }

    [Display(Name = "Tipo de unidad")]
    public IEnumerable<SelectListItem> FiltroTipoUnidad { get; set; }

    [Display(Name = "Modelo")]
    public string? FiltroModelo { get; set; }

    [Display(Name = "Año de fabricación")]
    [Range(1980, 2022, ErrorMessage = "Tenemos informaión para vehículos desde {1} hasta {2}")]
    public int? FiltroAnioFabricacion { get; set; }

    public List<IndexItemConsulta> Items { get; set; }
    public int CantidadItems { get; set; }
    public bool MostrarResultados { get; set; }

    public IndexViewModel() {
        FiltroMarca = new List<SelectListItem>();
        FiltroTipoUnidad = new List<SelectListItem>();

        Items = new List<IndexItemConsulta>();
        CantidadItems = 0;
        MostrarResultados = false;
    }
}

public class IndexItemConsulta {
    public int PlanillaId { get; set; }
    public string Detalle { get; set; }
    public int AnioFabricacion { get; set; }

    public IndexItemConsulta() {
        Detalle = "";
    }
}