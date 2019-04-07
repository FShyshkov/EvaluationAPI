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
    class ResultRepository : IRepository<Result>
    {
        readonly EvaluationContext _context;

        public ResultRepository(EvaluationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async virtual Task<Result> Add(Result entity)
        {
            await _context.Results.AddAsync(entity);
            return entity;
        }

        public async virtual Task<bool> Exist(int id)
        {
            return await _context.Results.AnyAsync(c => c.TestId == id);
        }

        public async virtual Task<Result> Find(int id)
        {
            return await _context.Results.FindAsync(id);
        }

        public virtual IEnumerable<Result> Get(System.Linq.Expressions.Expression<Func<Result, bool>> filter = null, Func<System.Linq.IQueryable<Result>, System.Linq.IOrderedQueryable<Result>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Result> query = _context.Set<Result>();

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

        public async virtual Task<Result> Remove(int id)
        {
            var entity = await _context.Results.SingleAsync();
            _context.Results.Remove(entity);
            return entity;
        }

        public async virtual Task<Result> Update(Result entity)
        {
            _context.Results.Update(entity);
            return entity;
        }
    }
}
