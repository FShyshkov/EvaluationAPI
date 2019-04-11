using EvaluationAPI.BLL.Requests;
using EvaluationAPI.BLL.Responses;
using EvaluationAPI.BLL.Contracts;
using System;

namespace EvaluationAPI.BLL.Contracts
{
    public interface IUserService : IUseCaseRequestHandler<RegisterUserRequest, RegisterUserResponse>, IUseCaseRequestHandler<LoginRequest, LoginResponse>, IDisposable
    {
    }
}