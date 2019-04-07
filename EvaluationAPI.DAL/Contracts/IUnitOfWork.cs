using EvaluationAPI.DAL.Repositories;
using EvaluationAPI.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace EvaluationAPI.DAL.Contracts
{
    public interface IUnitOfWork:IDisposable
    {
        ITestRepository Tests { get; }
        IQuestionRepository Questions { get; }
        IResultRepository Results { get; }

        Task<int> SaveAsync();
    }
}
