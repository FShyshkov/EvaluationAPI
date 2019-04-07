using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EvaluationAPI.DAL.Entities;

namespace EvaluationAPI.DAL.Contracts
{
    public interface IResultRepository
    {
        IEnumerable<Result> Get(Expression<Func<Result, bool>> filter = null,
            Func<IQueryable<Result>, IOrderedQueryable<Result>> orderBy = null,
            string includeProperties = "");
        Task<Result> Find(int id);
        Task<Result> Add(Result entity);
        Result Update(Result entity);
        Task<Result> Remove(int id);
        Task<bool> Exist(int id);
    }
}
