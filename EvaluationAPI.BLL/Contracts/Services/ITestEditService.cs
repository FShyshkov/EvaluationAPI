using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EvaluationAPI.BLL.DTO;

namespace EvaluationAPI.BLL.Contracts
{
    public interface ITestEditService:IDisposable
    {
        Task<ISingleResponse<TestDTO>> GetTestAsync(int id);

        Task<ISingleResponse<TestDTO>> AddTestAsync(string testName);

        Task<ISingleResponse<TestDTO>> UpdateTestAsync(int id, string testName);

        Task<ISingleResponse<QuestionDTO>> AddQuestionAsync(string QuestionText, string[] PossibleAnswers, int[] correctAnswers, int testId);

        Task<ISingleResponse<QuestionDTO>> UpdateQuestionAsync(int id, string QuestionText, string[] PossibleAnswers, int[] correctAnswers, int testId);

    }
}
