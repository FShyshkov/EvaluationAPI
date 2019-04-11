using System.Collections.Generic;

namespace EvaluationAPI.BLL.Responses
{
    public class RegisterUserResponse
    {
        public string Id { get; }
        public IEnumerable<string> Errors { get; }
        public bool Success { get; }
        public string Message { get; }


    public RegisterUserResponse(IEnumerable<string> errors, bool success = false, string message = null)
        {
            Errors = errors;
            Success = success;
            Message = message;
        }

        public RegisterUserResponse(string id, bool success = false, string message = null)
        {
            Id = id;
            Success = success;
            Message = message;
        }
    }
}
