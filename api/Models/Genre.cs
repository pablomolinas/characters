using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Models{
    public class Genre{        
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Nombre es un campo requerido.")]
        public string Name { get;set; }

        // se almacena ruta de la imagen o bien link
        [Display(Name ="Imagen")]
        public string Image { get;set;}
        
        public List<Movie> Movies { get; set; }
    }
}