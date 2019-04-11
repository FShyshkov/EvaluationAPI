
using EvaluationAPI.BLL.DTO;

namespace EvaluationAPI.Models.Responses
{
    public class LoginResponse
    {
        public AccessToken AccessToken { get; }

        public LoginResponse(AccessToken accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
