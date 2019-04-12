using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.DAL.Contracts;
using EvaluationAPI.BLL.Requests;
using EvaluationAPI.BLL.Responses;
using EvaluationAPI.DAL.Identity.Responses;
using EvaluationAPI.DAL.Identity.IdentityEntity;

namespace EvaluationAPI.BLL.Services
{
    class UserService : IUserService
    {
        private readonly IUnitOfWork _evalUOW;
        private readonly IJwtFactory _jwtFactory;
        private bool disposedValue = false;

        public UserService(IUnitOfWork uow, IJwtFactory jwtFactory)
        {
            _evalUOW = uow ?? throw new ArgumentNullException(nameof(uow));
            _jwtFactory = jwtFactory ?? throw new ArgumentNullException(nameof(uow));
        }

        //Register
        public async Task<bool> Handle(RegisterUserRequest message, IOutputPort<RegisterUserResponse> outputPort)
        {
            var response = await _evalUOW.Users.Create(message.FirstName, message.LastName, message.Email, message.UserName, message.Password);
            outputPort.Handle(response.Success ? new RegisterUserResponse(response.Id, true) : new RegisterUserResponse(response.Errors.Select(e => e.Description)));
            return response.Success;
        }

        public async Task<bool> Handle(LoginRequest message, IOutputPort<LoginResponse> outputPort)
        {
            if (!string.IsNullOrEmpty(message.UserName) && !string.IsNullOrEmpty(message.Password))
            {
                // ensure we have a user with the given user name
                var user = await _evalUOW.Users.FindByName(message.UserName);
                if (user != null)
                {
                    // validate password
                    if (await _evalUOW.Users.CheckPassword(user, message.Password))
                    {
                        // generate access token
                        outputPort.Handle(new LoginResponse(await _jwtFactory.GenerateEncodedToken(user.Id, user.UserName, (List<string>)await _evalUOW.Users.GetRoles(user)), true));
                        return true;
                    }
                }
            }
            outputPort.Handle(new LoginResponse(new[] { new Error("login_failure", "Invalid username or password.") }));
            return false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _evalUOW.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
