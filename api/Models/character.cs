using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api{
    class Character{
        public int Id { get; set; }
        
        [MaxLength(120)]
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Nombre es requerido.")]
        public string Name { get;set; }

        [Required(ErrorMessage = "Edad es requerido.")]
        [Range(1, 99999)]
        public int Age { get;set; }

        [Required(ErrorMessage = "Peso es requerido.")]
        [Range(1, 9999)]
        public float Weight { get;set; }
        
        [Display(Name = "Historia")]
        [MaxLength(1000, ErrorMessage = "La história puede tener 1000 caracteres como máximo.")]
        public string Story { get;set; }

        // se almacena ruta de la imagen o bien link
        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "Imagen es un campo requerido.")]
        public string Image { get;set;}

        public List<Movie> Movies { get; set; }
    }
}