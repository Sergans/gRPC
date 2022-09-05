using ClinicService.Models;
using Grpc.Core;

namespace ClinicService.Services
{
    public interface IAuthenticateService
    {
        AuthenticationResponse Login(AuthenticationRequest authenticationRequest);

        public SessionInfo GetSessionInfo(string sessionToken);
    }
}
