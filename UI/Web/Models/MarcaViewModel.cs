using System;
using System.ComponentModel.DataAnnotations;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.UI.Web.Models {
    public class MarcaViewModel {

        [Display(Name = "Código")]
        public int MarcaId { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar el Nombre")]
        public string Detalle { get; set; }
        
        [Display(Name = "Activa?")]
        public bool Activo { get; set; }

        public MarcaViewModel() {}

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
}