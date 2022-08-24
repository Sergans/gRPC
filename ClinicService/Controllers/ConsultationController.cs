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
    public class ConsultationController : ControllerBase
    {
        #region Serives

        private readonly IConsultationRepository _consultationRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IPetRepository _petRepository;
        private readonly ILogger<ConsultationController> _logger;

        #endregion

        #region Constructors

        public ConsultationController(IConsultationRepository consultationRepository, IClientRepository clientRepository, IPetRepository petRepository,
            ILogger<ConsultationController> logger)
        {
            _logger = logger;
            _consultationRepository = consultationRepository;
            _clientRepository=clientRepository;
            _petRepository=petRepository;
        }

        #endregion

        #region Public Methods

        [HttpPost("create")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateConsultationRequest createRequest)
        {
            Client client = new Client()
            {
                Document = createRequest.Document,
                FirstName = createRequest.FirstName,
                Surname = createRequest.Surname,
                Patronymic = createRequest.Patronymic,
            };
            var clientId = _clientRepository.Add(client);
            Pet pet = new Pet()
            {
                Name = createRequest.Name,
                Birthday = createRequest.Birthday,
                ClientId= clientId,
            };
            var petId = _petRepository.Add(pet);
            Consultation consultation = new Consultation()
            {
                ConsultationDate = DateTime.Now,
                Description = createRequest.Description,
                ClientId = clientId,
                PetId = petId,

            };
            _consultationRepository.Add(consultation);
           

            return Ok();
        }
           

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

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] int consultationId)
        {
            _consultationRepository.Delete(consultationId);
            return Ok();
        }

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(IList<Client>), StatusCodes.Status200OK)]
        public IActionResult GetAll() =>
            Ok(_consultationRepository.GetAll());

        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        public IActionResult GetById([FromRoute] int consultationId) =>
            Ok(_consultationRepository.GetById(consultationId));


        #endregion
    }
}
