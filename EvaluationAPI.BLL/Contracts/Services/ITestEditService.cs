using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EvaluationAPI.BLL.DTO;

namespace EvaluationAPI.BLL.Contracts
{
    public interface ITestEditService:IDisposable
    {
        Task<ISingleResponse<TestDTO>> GetTestAsync(long id);

        Task<ISingleResponse<TestDTO>> UpdateTestAsync(TestDTO test);

        Task<ISingleResponse<QuestionDTO>> AddQuestionAsync(QuestionDTO question);

        Task<ISingleResponse<TestDTO>> DeleteTestAsync(long id);
    }
}
