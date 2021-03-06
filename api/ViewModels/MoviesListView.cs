using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using System.ComponentModel.DataAnnotations;

namespace api.ViewModels
{
    public class MoviesListView
    {
        // Vista para objeto Movies,
        // permite serializar solo Name e Image del objeto y asi restringir la informacion que envia un endpoint.

        public string Title { get; set; }
        public string Image { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }

        public MoviesListView(Movie movie)
        {
            this.Image = movie.Image;
            this.Title = movie.Title;
            this.Date = movie.Date;
        }
    }
}
