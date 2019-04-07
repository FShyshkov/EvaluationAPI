using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.DAL.Identity.IdentityEntity;
using EvaluationAPI.DAL.Contracts;

namespace EvaluationAPI.BLL.Services
{
    class UserService : IUserService
    {
        private readonly IUnitOfWork _evalUOW;
        private bool disposedValue = false;

        public UserService(IUnitOfWork uow)
        {
            _evalUOW = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public Task AddAsync(EvaluationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<EvaluationUser> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _evalUOW.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
