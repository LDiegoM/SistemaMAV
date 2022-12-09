using System;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.ViewModels.Consultar;

public class ItemsViewModel {
    public Planilla? Planilla;
    public ICollection<ConsultaPlanillaItemViewModel> Items;

    public VehiculoViewModel? Vehiculo;

    public ItemsViewModel() {
        Planilla = null;
        Items = new List<ConsultaPlanillaItemViewModel>();
        Vehiculo = null;
    }
}

public class ConsultaPlanillaItemViewModel : SistemaMAV.Web.ViewModels.PlanillaItemViewModel {
    public bool HayStock;

    public ConsultaPlanillaItemViewModel(PlanillaItem planillaItem) : base(planillaItem) {
        HayStock = false;
    }
}
