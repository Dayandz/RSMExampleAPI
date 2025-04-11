using DBZ.Infraestructure.Models;

namespace DBZ.Infraestructure.Services
{
    public interface IDragonBallApiService
    {
        Task<IEnumerable<Character>> GetCharactersAsync();
        Task<IEnumerable<Transformation>> GetTransformationsAsync();

    }
}
