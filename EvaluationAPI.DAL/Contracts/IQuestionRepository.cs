using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EvaluationAPI.DAL.Entities;

namespace EvaluationAPI.DAL.Contracts
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> Get(Expression<Func<Question, bool>> filter = null,
            Func<IQueryable<Question>, IOrderedQueryable<Question>> orderBy = null,
            string includeProperties = "");
        Task<Question> Find(int id);
        Task<Question> Add(Question entity);
        Question Update(Question entity);
        Task<Question> Remove(int id);
        Task<bool> Exist(int id);
    }
}
