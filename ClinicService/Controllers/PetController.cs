using ClinicService.Data;
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

        //[HttpPost("create")]
        //[ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        //public IActionResult Create([FromBody] CreateClientRequest createRequest) =>
        //    Ok(_clientRepository.Add(new Client
        //    {
        //        Document = createRequest.Document,
        //        Surname = createRequest.Surname,
        //        FirstName = createRequest.FirstName,
        //        Patronymic = createRequest.Patronymic
        //    }));

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

        //[HttpGet("get-all")]
        //[ProducesResponseType(typeof(IList<Client>), StatusCodes.Status200OK)]
        //public IActionResult GetAll() =>
        //    Ok(_clientRepository.GetAll());

        //[HttpGet("get/{id}")]
        //[ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        //public IActionResult GetById([FromRoute] int clientId) =>
        //    Ok(_clientRepository.GetById(clientId));


        #endregion
    }
}
