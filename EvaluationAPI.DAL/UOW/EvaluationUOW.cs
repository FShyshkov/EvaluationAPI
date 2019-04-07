using System;
using System.Threading.Tasks;
using EvaluationAPI.DAL.Contracts;
using EvaluationAPI.DAL.Context;
using EvaluationAPI.DAL.Repositories;
using EvaluationAPI.DAL.Entities;

namespace EvaluationAPI.DAL.UOW
{
    class EvaluationUOW : IUnitOfWork
    {
        readonly EvaluationContext _context;
        internal TestRepository tests;
        internal QuestionRepository questions;
        internal ResultRepository results;
              
        private bool disposed = false;

        public EvaluationUOW(EvaluationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual ITestRepository Tests {
            get
            {

                if (this.tests == null)
                {
                    this.tests = new TestRepository(_context);
                }
                return tests;
            }
        }

        public virtual IQuestionRepository Questions
        {
            get
            {

                if (this.questions == null)
                {
                    this.questions = new QuestionRepository(_context);
                }
                return questions;
            }
        }

        public virtual IResultRepository Results
        {
            get
            {

                if (this.results == null)
                {
                    this.results = new ResultRepository(_context);
                }
                return results;
            }
        }        

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
