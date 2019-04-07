using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EvaluationAPI.DAL.Identity.Entities;

namespace EvaluationAPI.BLL.Contracts
{
    public interface IUserService
    {
        Task AddAsync(EvaluationUser user);

        Task<EvaluationUser> GetByIdAsync(string id);
    }
}
