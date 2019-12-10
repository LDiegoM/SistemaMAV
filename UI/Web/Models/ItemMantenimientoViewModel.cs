using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.UI.Web.Models {
    public class ItemMantenimientoViewModel {

        [Display(Name = "Código")]
        public int ItemMantenimientoId { get; set; }

        [Display(Name = "Detalle")]
        [Required(ErrorMessage = "Debe ingresar el Detalle")]
        public string Detalle { get; set; }
        
        [Display(Name = "Km. predeterminados")]
        public int? KilometrosPredeterminado { get; set; }

        [Display(Name = "Tiempo pred. (meses)")]
        public int? TiempoPredeterminado { get; set; }

        [Display(Name = "Planillas de Mantenimiento")]
        public ICollection<PlanillaItem> PlanillaItems { get; set; }

        public ItemMantenimientoViewModel() {}

        public ItemMantenimientoViewModel(ItemMantenimiento itemMantenimiento) {
            ItemMantenimientoId = itemMantenimiento.ItemMantenimientoId;
            Detalle = itemMantenimiento.Detalle;
            KilometrosPredeterminado = itemMantenimiento.KilometrosPredeterminado;
            TiempoPredeterminado = itemMantenimiento.TiempoPredeterminado;
        }

        public ItemMantenimiento ToItemMantenimiento() {
            return new ItemMantenimiento() {
                ItemMantenimientoId = ItemMantenimientoId,
                Detalle = Detalle,
                KilometrosPredeterminado = KilometrosPredeterminado,
                TiempoPredeterminado = TiempoPredeterminado
            };
        }
    }
}