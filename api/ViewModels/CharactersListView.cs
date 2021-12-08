using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.ViewModels
{
    // Vista para objeto Character,
    // permite serializar solo Name e Image del objeto y asi restringir la informacion que envia un endpoint.

    public class CharactersListView
    {
        public string Name { get; set; }
        public string Image { get; set; }

        public CharactersListView(Character character)
        {
            this.Name = character.Name;
            this.Image = character.Image;
        }
    }
}
