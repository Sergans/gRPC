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
            Consultation consultation = new Consultation();
            var client=_clientRepository.GetById(createRequest.ClientId);
            var pet=_petRepository.GetById(createRequest.PetId);
            if (client != null)
            {
                if (pet== null)
                {
                   var PetId= _petRepository.Add(new Pet()
                    {
                        ClientId = client.ClientId,
                        Birthday = createRequest.Birthday,
                        Name = createRequest.Name,
                    });
                    consultation.PetId = PetId;
                }
                else
                {
                    consultation.PetId = pet.PetId;
                }
                
                consultation.Client = client;
                consultation.ConsultationDate = DateTime.Now;
                consultation.Description = createRequest.Description;
                
            }
            else
            {
                var clientId = _clientRepository.Add(new Client()
                {
                    Document=createRequest.Document,
                    FirstName=createRequest.FirstName,
                    Surname=createRequest.Surname,
                    Patronymic=createRequest.Patronymic,
                });
                var petId = _petRepository.Add(new Pet()
                {
                    Name = createRequest.Name,
                    Birthday = createRequest.Birthday,
                    ClientId = clientId,
                });
                
                consultation.PetId = petId;
                consultation.ClientId= clientId;
                consultation.Description= createRequest.Description;
                consultation.ConsultationDate= DateTime.Now;
            }
           
            _consultationRepository.Add(consultation);
           
            return Ok();
        }


        [HttpPut("update")]
        public IActionResult Update([FromBody] UpdateConsultationRequest updateRequest)
        {
            _consultationRepository.Update(new Consultation
            {
                ConsultationId = updateRequest.ConsultationId,
                ClientId = updateRequest.ClientId,
                PetId = updateRequest.PetId,
                ConsultationDate = updateRequest.ConsultationDate,
                Description = updateRequest.Description
            });
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] int consultationId)
        {
            _consultationRepository.Delete(consultationId);
            return Ok();
        }

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(IList<Consultation>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var consultation = _consultationRepository.GetAll();
            
            return Ok(consultation);
        }
            

        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        public IActionResult GetById([FromRoute] int consultationId) =>
            Ok(_consultationRepository.GetById(consultationId));


        #endregion
    }
}
