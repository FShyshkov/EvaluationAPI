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
        readonly ITestRepository tests;
        readonly IQuestionRepository questions;
        readonly IResultRepository results;
        readonly IUserRepository users;

        private bool disposed = false;

        public EvaluationUOW(EvaluationContext context, EvIdentityContext identContext, UserManager<EvaluationUser> appUserManager, IUserRepository userRepository, IResultRepository resultRepository, IQuestionRepository questionRepository, ITestRepository testRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _identContext = identContext ?? throw new ArgumentNullException(nameof(identContext));
            _appUserManager = appUserManager ?? throw new ArgumentNullException(nameof(appUserManager));
            users = userRepository ?? throw new ArgumentNullException(nameof(appUserManager));
            results = resultRepository ?? throw new ArgumentNullException(nameof(appUserManager));
            questions = questionRepository ?? throw new ArgumentNullException(nameof(appUserManager));
            tests = testRepository ?? throw new ArgumentNullException(nameof(appUserManager));
        }

        public IUserRepository Users
        {
            get
            {
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
                return tests;
            }
        }

        public virtual IQuestionRepository Questions
        {
            get
            {
                return questions;
            }
        }

        public virtual IResultRepository Results
        {
            get
            {
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
