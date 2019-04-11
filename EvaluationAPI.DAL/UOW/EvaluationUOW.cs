using System;
using System.Threading.Tasks;
using EvaluationAPI.DAL.Contracts;
using EvaluationAPI.DAL.Context;
using EvaluationAPI.DAL.Repositories;
using EvaluationAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using EvaluationAPI.DAL.Identity.IdentityContext;
using EvaluationAPI.DAL.Identity.IdentityEntity;
using EvaluationAPI.DAL.Identity;
using Microsoft.AspNetCore.Identity;

namespace EvaluationAPI.DAL.UOW
{
    class EvaluationUOW : IUnitOfWork
    {
        readonly EvaluationContext _context;
        readonly EvIdentityContext _identContext;
        readonly UserManager<EvaluationUser> _appUserManager;
        internal TestRepository tests;
        internal QuestionRepository questions;
        internal ResultRepository results;
        internal UserRepository users;

        private bool disposed = false;

        public EvaluationUOW(EvaluationContext context, EvIdentityContext identContext, UserManager<EvaluationUser> appUserManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _identContext = identContext;
            _appUserManager = appUserManager;
        }

        public IUserRepository Users
        {
            get
            {

                if (this.users == null)
                {
                    this.users = new UserRepository(_appUserManager, _identContext);
                }
                return users;
            }
        }

        public async virtual Task<IDbContextTransaction> StartTransaction()
        {
            return await _context.Database.BeginTransactionAsync();
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
