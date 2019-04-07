using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EvaluationAPI.DAL.Contracts
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        Task<TEntity> Find(int id);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Remove(int id);
        Task<bool> Exist(int id);
    }
}
