using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using EvaluationAPI.BLL.Requests;
using EvaluationAPI.BLL.Services;
using EvaluationAPI.Models.Settings;
using EvaluationAPI.Presenters;
using EvaluationAPI.BLL.Contracts;

namespace EvaluationAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IUserService _loginUseCase;
        private readonly LoginPresenter _loginPresenter;
        private readonly AuthSettings _authSettings;

        public AccessController(IUserService loginUseCase, IOptions<AuthSettings> authSettings)
        {
            _loginUseCase = loginUseCase;
            _loginPresenter = new LoginPresenter();
            _authSettings = authSettings.Value;
        }

        // POST api/v1/login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Models.Requests.LoginRequest request)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            await _loginUseCase.Handle(new LoginRequest(request.UserName, request.Password, Request.HttpContext.Connection.RemoteIpAddress?.ToString()), _loginPresenter);
            return _loginPresenter.ContentResult;
        }
    }
}