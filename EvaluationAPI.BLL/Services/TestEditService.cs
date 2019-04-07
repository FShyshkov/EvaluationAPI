using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.BLL.DTO;
using EvaluationAPI.DAL.Contracts;

namespace EvaluationAPI.BLL.Services
{
    class TestEditService : ITestEditService
    {
        private readonly IUnitOfWork _evalUOW;
        private bool disposedValue = false;

        public TestEditService(IUnitOfWork uow)
        {
            _evalUOW = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public Task<ISingleResponse<QuestionDTO>> AddQuestionAsync(QuestionDTO question)
        {
            throw new NotImplementedException();
        }

        public Task<ISingleResponse<TestDTO>> DeleteTestAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ISingleResponse<TestDTO>> GetTestAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ISingleResponse<TestDTO>> UpdateTestAsync(TestDTO test)
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
