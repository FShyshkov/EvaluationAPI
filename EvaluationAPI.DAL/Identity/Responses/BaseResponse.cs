using System.Collections.Generic;

namespace EvaluationAPI.DAL.Identity.Responses
{
    public abstract class BaseGatewayResponse
    {
        public bool Success { get; }
        public IEnumerable<Error> Errors { get; }

        protected BaseGatewayResponse(bool success = false, IEnumerable<Error> errors = null)
        {
            Success = success;
            Errors = errors;
        }
    }
}
