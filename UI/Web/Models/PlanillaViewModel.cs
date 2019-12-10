using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.UI.Web.Models {
    public class PlanillaViewModel {

        [Display(Name = "Código")]
        public int PlanillaId { get; set; }

        [Display(Name = "Modelo")]
        [Required(ErrorMessage = "Debe ingresar el Modelo")]
        public int ModeloId { get; set; }
        [Display(Name = "Modelo")]
        public Modelo Modelo { get; set; }        

        [Display(Name = "Detalle")]
        [Required(ErrorMessage = "Debe ingresar el Detalle de la Planilla")]
        public string Detalle { get; set; }

        [Display(Name = "Año Fabric.")]
        public int? AnioFabricacion { get; set; }

        [Display(Name = "Versión")]
        public int? Version { get; set; }

        [Display(Name = "Activa?")]
        public bool Activo { get; set; }

        public ICollection<PlanillaItem> PlanillaItems { get; set; }

        public PlanillaViewModel() {}

        public PlanillaViewModel(Planilla planilla) {
            PlanillaId = planilla.PlanillaId;
            ModeloId = planilla.ModeloId;
            Modelo = planilla.Modelo;
            Detalle = planilla.Detalle;
            AnioFabricacion = planilla.AnioFabricacion;
            Version = planilla.Version;
            Activo = planilla.Activo;
        }

        public Planilla ToPlanilla() {
            return new Planilla() {
                PlanillaId = PlanillaId,
                ModeloId = ModeloId,
                Modelo = Modelo,
                Detalle = Detalle,
                AnioFabricacion = AnioFabricacion,
                Version = Version,
                Activo = Activo
            };
        }
    }
}