using EvaluationAPI.DAL.Repositories;
using EvaluationAPI.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace EvaluationAPI.DAL.Contracts
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<Test> Tests { get; }
        IRepository<Question> Questions { get; }
        IRepository<Result> Results { get; }

        Task<int> SaveAsync();
    }
}
