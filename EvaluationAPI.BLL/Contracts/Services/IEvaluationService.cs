using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EvaluationAPI.BLL.DTO;
using EvaluationAPI.DAL.Entities;
using EvaluationAPI.BLL.Common;

namespace EvaluationAPI.BLL.Contracts
{
    public interface IEvaluationService:IDisposable
    {
        Task<IPagedResponse<TestDTO>> GetTestsSummaryAsync(int pageSize = 10, int pageNumber = 1);

        Task<ISingleResponse<TestDTO>> GetTestAsync(int id);

        Task<IPagedResponse<ResultDTO>> GetResultsForUserByTestAsync(string userName, int testId, int pageSize = 10, int pageNumber = 1);

        Task<IPagedResponse<ResultDTO>> GetResultsByUserAsync(string userName, int pageSize = 10, int pageNumber = 1);

        Task<ISingleResponse<ResultDTO>> AddResultForTestAsync(int testId, string userName, int result);
    }
}
