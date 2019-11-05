using System;

namespace SistemaMAV.Entities.Models {
    public class Marca {
        public int MarcaId { get; set; }

        public string Detalle { get; set; }
        
        public bool Activo { get; set; }
    }
}