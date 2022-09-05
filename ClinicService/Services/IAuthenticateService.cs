﻿using ClinicService.Models;
using ClinicService.Models.Requests;
using ClinicService.Models.Responses;
using Grpc.Core;

namespace ClinicService.Services
{
    public interface IAuthenticateService
    {
        AuthenticationResponse Login(AuthenticationRequest authenticationRequest);

        public SessionInfo GetSessionInfo(string sessionToken);
    }
}
