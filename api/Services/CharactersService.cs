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
    // Capa de servicio para personajes, se trabaja con la logica y validaciones,
    // permite simplificar el codigo en los endpoints
    public class CharactersService : ICharacterService
    {
        private readonly ICharacterRepository _charactersRepository;
        private readonly ICharacterMovieRepository _characterMovieRepository;

        public CharactersService(ICharacterRepository charactersRepository, ICharacterMovieRepository characterMovieRepository)
        {
            this._charactersRepository = charactersRepository;
            this._characterMovieRepository = characterMovieRepository;
        }
        
        public async Task<Result> GetCharacterById(int id)
        {
            try
            {
                var character = await this._charactersRepository.GetByIdAsync(id);
                if (character != null)
                {
                    // carga de peliculas o series asociadas
                    character.CharacterMovies = await this._characterMovieRepository.GetMoviesByCharacterId(character.CharacterId);
                    
                    return Result<Character>.SuccessResult(character);
                }
            }catch(Exception e)
            {
                return Result.ExceptionResult(e);
            }

            return Result.FailureResult("Personaje inexistente.");
        }

        public async Task<Result> GetCharacterList(string? name, int? age, int? movie)
        {
            try { 
                var results = await this._charactersRepository.GetListAsync();
                if (results != null)
                {
                    // carga de peliculas o series asociadas
                    foreach (Character c in results)
                    {                        
                        c.CharacterMovies = await this._characterMovieRepository.GetMoviesByCharacterId(c.CharacterId);
                    }
                    
                    // filtros
                    var r = this._charactersRepository.FilterCharacterByName(results, name);
                    r = this._charactersRepository.FilterCharacterByAge(r, age);
                    r = this._charactersRepository.FilterCharacterByMovieId(r, movie);

                    // Convierto a vista, para enviar solo los campos que deseo mostrar
                    var view = new List<CharactersListView>();

                    foreach (Character c in r)
                    {
                        view.Add(new CharactersListView(c));
                    }
                    return Result<List<CharactersListView>>.SuccessResult(view);
                }
            }catch (Exception e)
            {
                return Result.ExceptionResult(e);
            }

            return Result.FailureResult("Sin resultados");
        }

        public async Task<Result> AddCharacter(Character character)
        {
            try { 
                if (character == null) { return Result.FailureResult("Datos invalidos."); }

                var result = await this._charactersRepository.AddAsync(character);
                if (result)
                {
                    return Result<string>.SuccessResult("Personaje creado exitosamente.");
                }

            }catch (Exception e)
            {
                return Result.ExceptionResult(e);
            }

            return Result.FailureResult("Personaje no creado.");
        }

        public async Task<Result> UpdateCharacter(Character character)
        {
            try { 
                if (this._charactersRepository.GetByIdAsync(character.CharacterId) == null) { return Result.FailureResult("El personaje enviado no existe."); }

                var r = await this._charactersRepository.UpdateAsync(character);
                if (r)
                {
                    return Result<string>.SuccessResult("Personaje actualizado con exito.");
                }

            }catch (Exception e)
            {
                return Result.ExceptionResult(e);
            }

            return Result.FailureResult("Personaje no actualizado.");
        }

        public async Task<Result> DeleteCharacter(int id)
        {
            try { 
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

            }catch (Exception e)
            {
                return Result.ExceptionResult(e);
            }

            return Result.FailureResult("Id de personaje invalido.");
        }

        public async Task<Result> ExistCharacter(int id)
        {
            try { 
                var character = await this._charactersRepository.GetByIdAsync(id);
                if (character != null)
                {
                    return Result.SuccessResult();
                }

            }catch (Exception e)
            {
                return Result.ExceptionResult(e);
            }

            return Result.FailureResult("Personaje inexistente.");
        }
    }    
}
