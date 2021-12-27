using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ProjectmongoDB.Application.Convert;
using ProjectmongoDB.Application.Interfaces;
using ProjectmongoDB.Application.Model;
using ProjectmongoDB.Application.Model.Request;
using ProjectmongoDB.Application.Model.Response;

namespace ProjectMongoDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectmongoDBController : ControllerBase
    {
        private readonly IProjectMongoDBRepository _projectMongoDBService;

        public ProjectmongoDBController(IProjectMongoDBRepository projectMongoDBService)
        {
            _projectMongoDBService = projectMongoDBService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Insert(PersonInsertRequest personInsertRequest, CancellationToken cancellationToken)
        {
            if (personInsertRequest.IsValid())
                return BadRequest();

            await _projectMongoDBService.InsertAsync(personInsertRequest.ToPersonInsertModelConvert(), cancellationToken);

            return Created("", "");
        }

        [HttpPut]        
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(PersonUpdateRequest personRequest, CancellationToken cancellationToken)
        {
            if (personRequest.IsValid())
                return BadRequest();

            await _projectMongoDBService.UpdateAsync(personRequest.ToPersonUpdateModelConvert(), cancellationToken);

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PersonModel>), StatusCodes.Status200OK)]        
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _projectMongoDBService.GetAllDataAsync(cancellationToken);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("{personId}")]
        [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string personId ,CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(personId))
                return BadRequest();

            var response = await _projectMongoDBService.GetByIdAsync(ObjectId.Parse(personId), cancellationToken);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("cpf/{cpf}")]
        [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCpf(string cpf, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(cpf))
                return BadRequest();

            var response = await _projectMongoDBService.GetByCpfAsync(cpf, cancellationToken);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]        
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string cpf, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(cpf))
                return BadRequest();

            await _projectMongoDBService.DeleteAsync(cpf, cancellationToken);

            return Ok();
        }
    }
}