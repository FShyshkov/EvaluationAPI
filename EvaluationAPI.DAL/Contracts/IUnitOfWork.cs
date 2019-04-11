using EvaluationAPI.DAL.Repositories;
using EvaluationAPI.DAL.Entities;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace EvaluationAPI.DAL.Contracts
{
    public interface IUnitOfWork:IDisposable
    {
        ITestRepository Tests { get; }
        IQuestionRepository Questions { get; }
        IResultRepository Results { get; }
        IUserRepository Users { get; }

        Task<IDbContextTransaction> StartTransaction();
        Task<int> SaveAsync();
    }
}
