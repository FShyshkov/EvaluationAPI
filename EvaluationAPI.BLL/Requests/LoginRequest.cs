using System;
using System.Collections.Generic;
using System.Text;
using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.BLL.Responses;

namespace EvaluationAPI.BLL.Requests
{
    public class LoginRequest: IUseCaseRequest<LoginResponse>
    {
        public string UserName { get; }
        public string Password { get; }
        public string RemoteIpAddress { get; }

        public LoginRequest(string userName, string password, string remoteIpAddress)
        {
            UserName = userName;
            Password = password;
            RemoteIpAddress = remoteIpAddress;
        }
    }
}
