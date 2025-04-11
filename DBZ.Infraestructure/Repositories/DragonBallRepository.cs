using DBZ.Infraestructure.Data;
using DBZ.Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DBZ.Infraestructure.Repositories
{
    public class DragonBallRepository : IDragonBallRepository
    {
        private readonly DragonBallDbContext _context;

        public DragonBallRepository(DragonBallDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await _context.Characters.Include(c => c.Transformations).ToListAsync();
        }

        public async Task<Character> GetCharacterByIdAsync(int id)
        {
            return await _context.Characters.Include(c => c.Transformations).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Character>> GetCharactersByNameAsync(string name)
        {
            return await _context.Characters
                .Include(c => c.Transformations)
                .Where(c => c.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Character>> GetCharactersByAffiliationAsync(string affiliation)
        {
            return await _context.Characters
                .Include(c => c.Transformations)
                .Where(c => c.Affiliation == affiliation)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transformation>> GetAllTransformationsAsync()
        {
            return await _context.Transformations.ToListAsync();
        }

        public async Task<bool> AnyCharactersExistAsync()
        {
            return await _context.Characters.AnyAsync();
        }

        public async Task<bool> AnyTransformationsExistAsync()
        {
            return await _context.Transformations.AnyAsync();
        }

        public async Task AddCharactersAsync(IEnumerable<Character> characters)
        {
            await _context.Characters.AddRangeAsync(characters);
            await _context.SaveChangesAsync();
        }

        public async Task AddTransformationsAsync(IEnumerable<Transformation> transformations)
        {
            await _context.Transformations.AddRangeAsync(transformations);
            await _context.SaveChangesAsync();
        }

        public async Task ClearDatabaseAsync()
        {
            _context.Transformations.RemoveRange(_context.Transformations);
            _context.Characters.RemoveRange(_context.Characters);
            await _context.SaveChangesAsync();
        }
    }
}
