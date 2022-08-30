using ClinicService.Data;
using ClinicService.Protos;
using Grpc.Core;
using static ClinicService.Protos.PetService;

namespace ClinicService.Services.Impl
{
    public class PetService : PetServiceBase
    {
        #region Serives

        private readonly ClinicServiceDbContext _dbContext;
        private readonly ILogger<PetService> _logger;

        #endregion

        #region Constructors

        public PetService(ClinicServiceDbContext dbContext,
            ILogger<PetService> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        #endregion
        public override Task<ClinicService.Protos.CreatePetResponse> CreatePet(ClinicService.Protos.CreatePetRequest request, ServerCallContext context)
        {
            var pet = new Pet
            {
                ClientId = request.ClientId,
                Name = request.Name,
                Birthday = request.Birthday.ToDateTime(),

            };
            _dbContext.Pets.Add(pet);

            _dbContext.SaveChanges();

            var response = new CreatePetResponse
            {
                PetId = pet.PetId
            };

            return Task.FromResult(response);
        }
        public override Task<GetPetsResponse> GetPets(GetPetsRequest request, ServerCallContext context)
        {
            var response = new GetPetsResponse();
            response.Pets.AddRange(_dbContext.Pets.Select(pet => new PetResponse
            {
                PetId = pet.PetId,
                Name = pet.Name,
                Birthday = pet.Birthday.ToString(),

            }).ToList());

            return Task.FromResult(response);
        }
    }
}
