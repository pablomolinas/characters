using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Response;
using api.Models;
using api.ModelsViews;

namespace api.Services
{
    public class CharactersService : ICharacterService
    {
        private readonly ICharacterRepository _charactersRepository;

        public CharactersService(ICharacterRepository charactersRepository)
        {
            this._charactersRepository = charactersRepository;
        }

        // Capa de servicio para personajes, se trabaja con la logica y validaciones,
        // permite simplificar el codigo en los endpoints
        public async Task<Result> GetCharacterById(int id)
        {
            var character = await this._charactersRepository.GetByIdAsync(id);
            if (character != null)
            {
                return Result<Character>.SuccessResult(character);
            }
            return Result.FailureResult("Personaje inexistente.");
        }

        public async Task<Result> GetCharacterList()
        {
            var results = await this._charactersRepository.GetListAsync();
            if (results != null)
            {
                // Convierto a vista, para enviar solo los campos que deseo mostrar
                var view = new List<CharactersListView>();

                foreach (Character c in results)
                {
                    view.Add(new CharactersListView(c));
                }
                return Result<List<CharactersListView>>.SuccessResult(view);
            }

            return Result.FailureResult("Sin resultados");
        }

        public async Task<Result> AddCharacter(Character character)
        {
            if (character == null) { return Result.FailureResult("Datos invalidos."); }

            var result = await this._charactersRepository.AddAsync(character);
            if (result != null)
            {
                return Result<string>.SuccessResult("Personaje creado exitosamente.");
            }

            return Result.FailureResult("Personaje no creado.");
        }

        public async Task<Result> UpdateCharacter(Character character)
        {
            if (this._charactersRepository.GetByIdAsync(character.CharacterId) == null) { return Result.FailureResult("El personaje enviado no existe."); }

            var r = await this._charactersRepository.UpdateAsync(character);
            if (r)
            {
                return Result<string>.SuccessResult("Personaje actualizado con exito.");
            }
            
            return Result.FailureResult("Personaje no actualizado.");
        }

        public async Task<Result> DeleteCharacter(int id)
        {
            var character = await this._charactersRepository.GetByIdAsync(id);
            if (character != null)
            {
                var r = await this._charactersRepository.DeleteAsync(character);
                if (r) 
                {
                    return Result<string>.SuccessResult("Personaje eliminado con exito.");
                }

                return Result.FailureResult("No fue posible eliminar Personaje.");
            }

            return Result.FailureResult("Id de personaje invalido.");
        }

        public async Task<Result> ExistCharacter(int id)
        {
            var character = await this._charactersRepository.GetByIdAsync(id);
            if (character != null)
            {
                return Result.SuccessResult();
            }
            
            return Result.FailureResult("Personaje inexistente.");
        }
    }    
}
