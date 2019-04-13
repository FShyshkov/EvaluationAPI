using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.BLL.DTO;
using EvaluationAPI.BLL.Exceptions;
using EvaluationAPI.BLL.Responses;
using EvaluationAPI.DAL.Contracts;
using EvaluationAPI.DAL.Entities;

namespace EvaluationAPI.BLL.Services
{
    public class TestEditService : ITestEditService
    {
        private readonly IDTOMapper _mapper;
        private readonly IUnitOfWork _evalUOW;
        private bool disposedValue = false;

        public TestEditService(IUnitOfWork uow, IDTOMapper mapper)
        {
            _evalUOW = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ISingleResponse<TestDTO>> GetTestAsync(int id)
        {
            var response = new SingleResponse<TestDTO>();

            try
            {
                var test = await _evalUOW.Tests.Get(x => x.TestId == id, null, "Questions");
                var testDTO = _mapper.MapTest(test.FirstOrDefault());

                response.Model = testDTO;
            }
            catch (Exception ex)
            {
                response.SetError(nameof(GetTestAsync), ex);
            }

            return response;
        }

        public async Task<ISingleResponse<TestDTO>> AddTestAsync(string testName)
        {
            var response = new SingleResponse<TestDTO>();
            var tempTest = new Test()
            {
                TestName = testName,
            };

            using (var transaction = await _evalUOW.StartTransaction())
            {
                try
                {
                    var tempt = await _evalUOW.Tests.Add(tempTest);
                    await _evalUOW.SaveAsync();
                    response.Model = _mapper.MapTest(tempt);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    response.SetError(nameof(AddTestAsync), ex);
                }
            }
            return response;
        }

        public async Task<ISingleResponse<TestDTO>> UpdateTestAsync(int id, string testName)
        {
            var response = new SingleResponse<TestDTO>();
            Test test = new Test
            {
                TestId = id,
                TestName = testName
            };
            using (var transaction = await _evalUOW.StartTransaction())
            {
                try
                {                                
                    _evalUOW.Tests.Update(test);
                    await _evalUOW.SaveAsync();
                    response.Model = _mapper.MapTest(test);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    response.SetError(nameof(AddTestAsync), ex);
                }
            }
            return response;
        }

        public async Task<ISingleResponse<QuestionDTO>> AddQuestionAsync(string questionText, string[] possibleAnswers, int[] correctAnswers, int testId)
        {
            List<int> tempList = new List<int>(correctAnswers);
            var response = new SingleResponse<QuestionDTO>();
            var tempQuestion = new QuestionDTO()
            {
                QuestionText = questionText,
                PossibleAnswers = possibleAnswers,
                Answer = tempList,
                TestId = testId
            };

            using (var transaction = await _evalUOW.StartTransaction())
            {
                try
                {
                    var tempq = await _evalUOW.Questions.Add(_mapper.MapDTOQuestion(tempQuestion));
                    await _evalUOW.SaveAsync();
                    response.Model = _mapper.MapQuestion(tempq);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    response.SetError(nameof(AddTestAsync), ex);
                }
            }
            return response;
        }

        public async Task<ISingleResponse<QuestionDTO>> UpdateQuestionAsync(int id, string questionText, string[] PossibleAnswers, int[] correctAnswers, int testId)
        {
            var response = new SingleResponse<QuestionDTO>();
            List<int> tempList = new List<int>(correctAnswers);
            QuestionDTO questionDTO = new QuestionDTO
            {
                QuestionId = id,
                QuestionText = questionText,
                TestId = testId,
                PossibleAnswers = PossibleAnswers,
                Answer = tempList
            };
            Question question = _mapper.MapDTOQuestion(questionDTO);
            using (var transaction = await _evalUOW.StartTransaction())
            {
                try
                {
                    _evalUOW.Questions.Update(question);
                    await _evalUOW.SaveAsync();
                    response.Model = _mapper.MapQuestion(question);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    response.SetError(nameof(AddTestAsync), ex);
                }
            }
            return response;
        }

        public async Task<IResponse> RemoveQuestionAsync(int id)
        {
            var response = new Response();

            try
            {
                // Retrieve order by id
                var entity = _evalUOW.Questions.Get(x => x.QuestionId == id, null, null).ToAsyncEnumerable().FirstOrDefault();

                if (entity == null)
                    return response;


                // Delete order
                var question = await _evalUOW.Questions.Remove(id);
                await _evalUOW.SaveAsync();
            }
            catch (Exception ex)
            {
                response.SetError(nameof(RemoveQuestionAsync), ex);
            }

            return response;
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
