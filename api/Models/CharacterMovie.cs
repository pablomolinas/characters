using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class CharacterMovie
    {
        public int CharacterId { get; set; }
        public Character Character { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}
