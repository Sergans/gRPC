// See https://aka.ms/new-console-template for more information
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using static ClientServiceProtos.ClientService;
using static PetServiceProtos.PetService;

AppContext.SetSwitch(
              "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
using var channel = GrpcChannel.ForAddress("http://localhost:5001");

ClientServiceClient client = new ClientServiceClient(channel);
PetServiceClient pet=new PetServiceClient(channel);
var createClientResponse = client.CreateClient(new ClientServiceProtos.CreateClientRequest
{
    Document = "PASS123",
    FirstName = "Сергей",
    Surname = "Сорокин",
    Patronymic = "Иванович"
});

Console.WriteLine($"Client ({createClientResponse.ClientId}) created successfully.");
var createPetResponse = pet.CreatePet(new PetServiceProtos.CreatePetRequest
{
    ClientId = (int)createClientResponse.ClientId,
    Birthday = Timestamp.FromDateTime(DateTime.UtcNow),
    Name = "Бобик"
}) ;

var getClientsResponse = client.GetClients(new ClientServiceProtos.GetClientsRequest());
if (getClientsResponse.ErrCode == 0)
{
    Console.WriteLine("Clients:");
    Console.WriteLine("========\n");
    foreach (var clientDto in getClientsResponse.Clients)
    {
        Console.WriteLine($"({clientDto.ClientId}/{clientDto.Document}) {clientDto.Surname} {clientDto.FirstName} {clientDto.Patronymic}");
    }
}
Console.WriteLine();
var getPetsResponse = pet.GetPets(new PetServiceProtos.GetPetsRequest());
if (getPetsResponse.ErrCode == 0)
{
    Console.WriteLine("Pets:");
    Console.WriteLine("========\n");
    foreach (var petDto in getPetsResponse.Pets)
    {
        Console.WriteLine($"({petDto.PetId}/{petDto.Name}) {petDto.Birthday} ");
    }
}
Console.ReadKey();