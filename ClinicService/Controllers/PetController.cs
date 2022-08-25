using ClinicService.Data;
using ClinicService.Models.Requests;
using ClinicService.Services;
using ClinicService.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        #region Serives

        private readonly IPetRepository _petRepository;
        private readonly ILogger<PetController> _logger;

        #endregion

        #region Constructors

        public PetController(IPetRepository petRepository,
            ILogger<PetController> logger)
        {
            _logger = logger;
            _petRepository = petRepository;
        }

        #endregion
        #region Public Methods

        [HttpPost("create")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreatePetRequest createRequest) =>
            Ok(_petRepository.Add(new Pet
            {
                Name = createRequest.Name,
                Birthday = createRequest.Birthday,
                ClientId = createRequest.ClientId,
            }));

        [HttpPut("update")]
        public IActionResult Update([FromBody] UpdatePetRequest updateRequest)
        {
            _petRepository.Update(new Pet
            {
                PetId = updateRequest.PetId,
                ClientId = updateRequest.ClientId,
                Birthday = updateRequest.Birthday,
                Name = updateRequest.Name,
               
            });
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] int petId)
        {
            _petRepository.Delete(petId);
            return Ok();
        }

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(IList<Pet>), StatusCodes.Status200OK)]
        public IActionResult GetAll() =>
            Ok(_petRepository.GetAll());

        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(Pet), StatusCodes.Status200OK)]
        public IActionResult GetById([FromRoute] int petId) =>
            Ok(_petRepository.GetById(petId));


        #endregion
    }
}
