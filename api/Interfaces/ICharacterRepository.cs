using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Repositories;

namespace api.Interfaces
{
    public interface ICharacterRepository : IGenericRepository<Character>
    {
        // Metodos particulares de Character
    }
}
