using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaMAV.UI.Web.Models {
    public class Marca {
        [Display(Name = "Código")]
        public int MarcaId { get; set; }

        [Display(Name = "Nombre")]
        public string Detalle { get; set; }
        
        [Display(Name = "Activa?")]
        public bool Activo { get; set; }
    }
}