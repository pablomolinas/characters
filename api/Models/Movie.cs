using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Models{
    public class Movie{ 
        [Key]
        public int MovieId { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "Title es requerido.")]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get;set; }
       
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "Fecha es requerida.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get;set; }
        
        // se almacena ruta de la imagen o bien link
        [Display(Name = "Imagen")]        
        public string Image { get;set; }

        // calificacion, del 1 al 5
        [Display(Name = "Calificación")]
        [Required(ErrorMessage = "Calificación es un campo requerido.")]
        [Range(1, 5)]
        public int Rating { get;set; }

        // Genero asociado
        [Display(Name = "Genero")]
        public int GenreId { get; set; }
        [Display(Name = "Genero")]
        public Genre Genre { get; set; }

        //
        [Display(Name = "Personajes asociados")]
        public ICollection<CharacterMovie> CharacterMovies { get; set; }
    }
}