using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaMAV.Models {
    public class Marca {
        public int MarcaId { get; set; }
        public string Detalle { get; set; }
        
        public bool Activo { get; set; }
    }
}