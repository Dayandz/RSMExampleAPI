using DBZ.Infraestructure.Models;
using DBZ.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APIDBZ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransformationsController : ControllerBase
    {
        private readonly IDragonBallRepository _repository;

        public TransformationsController(IDragonBallRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transformation>>> GetAll()
        {
            var transformations = await _repository.GetAllTransformationsAsync();
            return Ok(transformations);
        }
    }
}
