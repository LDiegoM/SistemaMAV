using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.ViewModels.Consultar;

public class StockViewModel {
    [Display(Name = "Item de Mantenimiento")]
    public string? ItemDetalle { get; set; }

    public List<SistemaMAV.Web.ViewModels.StockViewModel> Stock { get; set; }

    public StockViewModel() {
        Stock = new List<SistemaMAV.Web.ViewModels.StockViewModel>();
    }
}
