// See https://aka.ms/new-console-template for more information
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using static ClientServiceProtos.ClientService;
using static PetServiceProtos.PetService;

//AppContext.SetSwitch(
//              "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
using var channel = GrpcChannel.ForAddress("https://localhost:5001");

ClinicService.Protos.AuthenticateService.AuthenticateServiceClient authenticateServiceClient = new ClinicService.Protos.AuthenticateService.AuthenticateServiceClient(channel);


var authenticationResponse = authenticateServiceClient.Login(new ClinicService.Protos.AuthenticationRequest
{
    UserName = "ga@ya.ru",
    Password = "12345"
});

if (authenticationResponse.Status != 0)
{
    Console.WriteLine("Authentication error.");
    Console.ReadKey();
    return;
}

Console.WriteLine($"Session token: {authenticationResponse.SessionInfo.SessionToken}");



var credentials = CallCredentials.FromInterceptor((c, m) =>
{
    m.Add("Authorization",
        $"Bearer {authenticationResponse.SessionInfo.SessionToken}");
    return Task.CompletedTask;
});

var protectedChannel = GrpcChannel.ForAddress("https://localhost:5001",
        new GrpcChannelOptions
        {

            Credentials = ChannelCredentials.Create(new SslCredentials(), credentials)
        });



ClientServiceClient client = new ClientServiceClient(protectedChannel);
PetServiceClient pet = new PetServiceClient(protectedChannel);
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
});

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