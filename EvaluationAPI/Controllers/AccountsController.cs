using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EvaluationAPI.BLL.Requests;
using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.Presenters;

namespace EvaluationAPI.Controllers
{
    [Route("api/v1/Register")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _registerUserUseCase;
        private readonly RegisterUserPresenter _registerUserPresenter;

        public AccountsController(IUserService registerUserUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
            _registerUserPresenter = new RegisterUserPresenter();
        }

        // POST api/accounts
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Models.Requests.RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // TODO: verify model
            await _registerUserUseCase.Handle(new RegisterUserRequest(request.FirstName, request.LastName, request.Email, request.UserName, request.Password), _registerUserPresenter);
            return _registerUserPresenter.ContentResult;
        }
    }
}