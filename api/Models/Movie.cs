using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Models{
    public class Movie{        
        public int Id { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "Title es requerido.")]
        public string Title { get;set; }

        // Edad
        [Display(Name = "Edad")]
        public System.DateTime Date { get;set; }
        
        // se almacena ruta de la imagen o bien link
        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "Imagen es un campo requerido.")]
        public string Image { get;set;}

        // calificacion, del 1 al 5
        [Display(Name = "Calificación")]
        [Required(ErrorMessage = "Calificación es un campo requerido.")]
        public MovieRating Rating { get;set; }

        //
        [Display(Name = "Personajes asociados")]
        public List<Character> Characters { get; set; }

        // Una pelicula puede pertenecer a varios generos
        [Display(Name = "Genero/s")]
        public List<Genre> Genres { get; set; }
    }

    // 
    public enum MovieRating {
        one=1,
        two,
        three,
        four,
        five
    }
}