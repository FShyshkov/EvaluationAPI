using System.Collections.Generic;

namespace EvaluationAPI.DAL.Identity.Responses
{
    public sealed class CreateUserResponse : BaseGatewayResponse
    {
        public string Id { get; }
        public CreateUserResponse(string id, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            Id = id;
        }
    }
}