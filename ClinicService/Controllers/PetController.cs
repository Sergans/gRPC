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

        //[HttpPut("update")]
        //public IActionResult Update([FromBody] UpdateClientRequest updateRequest)
        //{
        //    _clientRepository.Update(new Client
        //    {
        //        ClientId = updateRequest.ClientId,
        //        Surname = updateRequest.Surname,
        //        FirstName = updateRequest.FirstName,
        //        Patronymic = updateRequest.Patronymic
        //    });
        //    return Ok();
        //}

        //[HttpDelete("delete")]
        //public IActionResult Delete([FromQuery] int clientId)
        //{
        //    _clientRepository.Delete(clientId);
        //    return Ok();
        //}

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(IList<Pet>), StatusCodes.Status200OK)]
        public IActionResult GetAll() =>
            Ok(_petRepository.GetAll());

        //[HttpGet("get/{id}")]
        //[ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        //public IActionResult GetById([FromRoute] int clientId) =>
        //    Ok(_clientRepository.GetById(clientId));


        #endregion
    }
}
