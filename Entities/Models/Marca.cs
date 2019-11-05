using System;
using System.ComponentModel.DataAnnotations;
//using System.Data.Entity.Spatial; 

namespace SistemaMAV.Entities.Models {
    public class Marca {
        [Key]
        public int MarcaId { get; set; }

        [Required]
        public string Detalle { get; set; }
        
        public bool Activo { get; set; }
    }
}