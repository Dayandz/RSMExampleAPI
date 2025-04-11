using DBZ.Infraestructure.Models;

namespace DBZ.Infraestructure.Repositories
{
    public interface IDragonBallRepository
    {
        Task<IEnumerable<Character>> GetAllCharactersAsync();
        Task<Character> GetCharacterByIdAsync(int id);
        Task<IEnumerable<Character>> GetCharactersByNameAsync(string name);
        Task<IEnumerable<Character>> GetCharactersByAffiliationAsync(string affiliation);
        Task<IEnumerable<Transformation>> GetAllTransformationsAsync();
        Task<bool> AnyCharactersExistAsync();
        Task<bool> AnyTransformationsExistAsync();
        Task AddCharactersAsync(IEnumerable<Character> characters);
        Task AddTransformationsAsync(IEnumerable<Transformation> transformations);
        Task ClearDatabaseAsync();
    }
}
