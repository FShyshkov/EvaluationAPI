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

        public Task<ISingleResponse<TestDTO>> GetTestAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ISingleResponse<TestDTO>> AddTestAsync(string testName)
        {
            throw new NotImplementedException();
        }

        public Task<ISingleResponse<TestDTO>> UpdateTestAsync(int id, string testName)
        {
            throw new NotImplementedException();
        }

        public Task<ISingleResponse<QuestionDTO>> AddQuestionAsync(string QuestionText, string[] PossibleAnswers, int[] correctAnswers, int testId)
        {
            throw new NotImplementedException();
        }

        public Task<ISingleResponse<QuestionDTO>> UpdateQuestionAsync(int id, string QuestionText, string[] PossibleAnswers, int[] correctAnswers, int testId)
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
