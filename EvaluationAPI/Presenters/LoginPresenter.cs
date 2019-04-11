using System.Net;
using EvaluationAPI.BLL.Responses;
using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.Serialization;

namespace EvaluationAPI.Presenters
{
    public sealed class LoginPresenter : IOutputPort<LoginResponse>
    {
        public JsonContentResult ContentResult { get; }

        public LoginPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(LoginResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.Unauthorized);
            ContentResult.Content = response.Success ? JsonSerializer.SerializeObject(new EvaluationAPI.Models.Responses.LoginResponse(response.AccessToken)) : JsonSerializer.SerializeObject(response.Errors);
        }
    }
}
