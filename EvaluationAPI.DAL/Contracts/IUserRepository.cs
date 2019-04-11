using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EvaluationAPI.DAL.Identity.IdentityEntity;
using EvaluationAPI.DAL.Identity.Responses;

namespace EvaluationAPI.DAL.Contracts
{
    public interface IUserRepository
    {
        Task<bool> CheckPassword(EvaluationUser user, string password);
        Task<EvaluationUser> FindByName(string userName);
        Task<CreateUserResponse> Create(string firstName, string lastName, string email, string userName, string password);
        Task<IList<string>> GetRoles(EvaluationUser user);
        void Update(EvaluationUser user);
    }
}
