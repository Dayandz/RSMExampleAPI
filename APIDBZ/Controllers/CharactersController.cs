using DBZ.Infraestructure.Models;
using DBZ.Infraestructure.Repositories;
using DBZ.Infraestructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIDBZ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly IDragonBallRepository _repository;

        public CharactersController(IDragonBallRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Character>>> GetAll()
        {
            var characters = await _repository.GetAllCharactersAsync();
            return Ok(characters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetById(int id)
        {
            var character = await _repository.GetCharacterByIdAsync(id);
            if (character == null)
            {
                return NotFound();
            }
            return Ok(character);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Character>>> GetByName([FromQuery] string name)
        {
            var characters = await _repository.GetCharactersByNameAsync(name);
            return Ok(characters);
        }

        [HttpGet("affiliation/{affiliation}")]
        public async Task<ActionResult<IEnumerable<Character>>> GetByAffiliation(string affiliation)
        {
            var characters = await _repository.GetCharactersByAffiliationAsync(affiliation);
            return Ok(characters);
        }

        [Authorize]
        [HttpPost("sync")]
        public async Task<IActionResult> Sync([FromServices] IDragonBallApiService apiService)
        {
            if (await _repository.AnyCharactersExistAsync() || await _repository.AnyTransformationsExistAsync())
            {
                return BadRequest("Database is not empty. Please clean up the data first.");
            }

            var characters = await apiService.GetCharactersAsync();
            var transformations = await apiService.GetTransformationsAsync();

            // Filter characters by race (only Saiyan)
            var saiyanCharacters = characters.Where(c => c.Race == "Saiyan").ToList();

            // Filter transformations by affiliation (only Z Fighter)
            var zFighterTransformations = transformations
                .Where(t => t.Character?.Affiliation == "Z Fighter")
                .ToList();

            await _repository.AddCharactersAsync(saiyanCharacters);
            await _repository.AddTransformationsAsync(zFighterTransformations);

            return Ok("Data synchronized successfully");
        }

        [Authorize]
        [HttpPost("clean")]
        public async Task<IActionResult> Clean()
        {
            await _repository.ClearDatabaseAsync();
            return Ok("Database cleaned successfully");
        }
    }
}
