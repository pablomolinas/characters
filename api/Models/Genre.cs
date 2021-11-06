using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Models{
    public class Genre{ 
        [Key]
        public int GenreId { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Nombre es un campo requerido.")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get;set; }

        // se almacena ruta de la imagen o bien link
        [Display(Name ="Imagen")]
        public string Image { get;set;}
        
        public List<Movie> Movies { get; set; }
    }
}