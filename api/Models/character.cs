using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Models{
    public class Character{
        [Key]
        public int CharacterId { get; set; }
                
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Nombre es requerido.")]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get;set; }

        [Required(ErrorMessage = "Edad es requerido.")]
        [Range(1, 99999)] // Rango amplio por ser personajes de ficcion
        public int Age { get;set; }

        [Required(ErrorMessage = "Peso es requerido.")]
        [Range(0.01, 9999)] // Rango amplio por ser personajes de ficcion
        public float Weight { get;set; }
        
        [Display(Name = "Historia")]
        [MaxLength(1000, ErrorMessage = "La história puede tener 1000 caracteres como máximo.")]
        public string Story { get;set; }

        // se almacena ruta de la imagen o bien link
        [Display(Name = "Imagen")]
        public string Image { get;set; }

        public ICollection<CharacterMovie> CharacterMovies { get; set; }
    }
}