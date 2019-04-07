﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvaluationAPI.DAL.Contracts;
using EvaluationAPI.DAL.Entities;
using EvaluationAPI.DAL.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EvaluationAPI.DAL.Repositories
{
    class TestRepository: IRepository<Test>
    {
        readonly EvaluationContext _context;

        public TestRepository(EvaluationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async virtual Task<Test> Add(Test entity)
        {
            await _context.Tests.AddAsync(entity);
            return entity;
        }

        public async virtual Task<bool> Exist(int id)
        {
            return await _context.Tests.AnyAsync(c => c.TestId == id);
        }

        public async virtual Task<Test> Find(int id)
        {
            return await _context.Tests.FindAsync(id);
        }

        public virtual IEnumerable<Test> Get(System.Linq.Expressions.Expression<Func<Test, bool>> filter = null, Func<System.Linq.IQueryable<Test>, System.Linq.IOrderedQueryable<Test>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Test> query = _context.Set<Test>();

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

        public async virtual Task<Test> Remove(int id)
        {
            var entity = await _context.Tests.SingleAsync();
            _context.Tests.Remove(entity);
            return entity;
        }

        public async virtual Task<Test> Update(Test entity)
        {
            _context.Tests.Update(entity);
            return entity;
        }
    }
}
