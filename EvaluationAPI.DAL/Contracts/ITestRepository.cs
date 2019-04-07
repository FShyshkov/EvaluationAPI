using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EvaluationAPI.DAL.Entities;

namespace EvaluationAPI.DAL.Contracts
{
    public interface ITestRepository
    {
        IEnumerable<Test> Get(Expression<Func<Test, bool>> filter = null,
            Func<IQueryable<Test>, IOrderedQueryable<Test>> orderBy = null,
            string includeProperties = "");
        Task<Test> Find(int id);
        Task<Test> Add(Test entity);
        Test Update(Test entity);
        Task<Test> Remove(int id);
        Task<bool> Exist(int id);
    }
}
