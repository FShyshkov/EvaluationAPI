using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvaluationAPI.DAL.Contracts;
using EvaluationAPI.DAL.Entities;
using EvaluationAPI.DAL.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EvaluationAPI.DAL.Repositories
{
    class QuestionRepository : IRepository<Question>
    {
        readonly EvaluationContext _context;

        public QuestionRepository(EvaluationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async virtual Task<Question> Add(Question entity)
        {
            await _context.Questions.AddAsync(entity);
            return entity;
        }

        public async virtual Task<bool> Exist(int id)
        {
            return await _context.Questions.AnyAsync(c => c.TestId == id);
        }

        public async virtual Task<Question> Find(int id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public virtual IEnumerable<Question> Get(System.Linq.Expressions.Expression<Func<Question, bool>> filter = null, Func<System.Linq.IQueryable<Question>, System.Linq.IOrderedQueryable<Question>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Question> query = _context.Set<Question>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public async virtual Task<Question> Remove(int id)
        {
            var entity = await _context.Questions.SingleAsync();
            _context.Questions.Remove(entity);
            return entity;
        }

        public async virtual Task<Question> Update(Question entity)
        {
            _context.Questions.Update(entity);
            return entity;
        }
    }
}
