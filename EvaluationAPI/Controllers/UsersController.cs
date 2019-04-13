using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EvaluationAPI.BLL.Requests;
using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.Presenters;
using Microsoft.Extensions.Options;
using EvaluationAPI.Models.Settings;
using EvaluationAPI.Models.Responses;
using Microsoft.AspNetCore.Authorization;

namespace EvaluationAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEvaluationService _evaluationService;
        private readonly RegisterUserPresenter _registerUserPresenter;
        private readonly LoginPresenter _loginPresenter;

        public UsersController(IUserService registerUserUseCase, IEvaluationService evaluationService, RegisterUserPresenter userPresenter, LoginPresenter loginPresenter, IOptions<AuthSettings> authSettings)
        {
            _userService = registerUserUseCase;
            _registerUserPresenter = userPresenter;
            _loginPresenter = loginPresenter;
            _evaluationService = evaluationService;
        }

        // POST api/v1.0/users/register
        [HttpPost("register")]
        public async Task<ActionResult> Post([FromBody] Models.Requests.RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // TODO: verify model
            await _userService.Handle(new RegisterUserRequest(request.FirstName, request.LastName, request.Email, request.UserName, request.Password), _registerUserPresenter);
            return _registerUserPresenter.ContentResult;
        }
        // POST api/v1.0/users/login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Models.Requests.LoginRequest request)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            await _userService.Handle(new LoginRequest(request.UserName, request.Password, Request.HttpContext.Connection.RemoteIpAddress?.ToString()), _loginPresenter);
            return _loginPresenter.ContentResult;
        }

        [Authorize(Policy = "ApiUser")]
        [HttpGet("results/{name}")]
        public async Task<IActionResult> GetResults(string name, int? pageSize = 10, int? pageNumber = 1)
        {            
            var response = await _evaluationService.GetResultsByUserAsync(name, (int)pageSize, (int)pageNumber);
            return response.ToHttpResponse();
        }
    }
}