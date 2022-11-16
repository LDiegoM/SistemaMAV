using System;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.ViewModels.Consultar;

public class ItemsViewModel {
    public Planilla? Planilla;
    public ICollection<PlanillaItemViewModel> Items;

    public ItemsViewModel() {
        Planilla = null;
        Items = new List<PlanillaItemViewModel>();
    }
}
