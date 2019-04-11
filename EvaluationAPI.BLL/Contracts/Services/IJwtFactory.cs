using System.Collections.Generic;
using System.Threading.Tasks;
using EvaluationAPI.BLL.DTO;


namespace EvaluationAPI.BLL.Contracts
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateEncodedToken(string id, string userName, List<string> roles);
    }
}