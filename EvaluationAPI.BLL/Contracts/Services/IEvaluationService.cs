using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EvaluationAPI.BLL.DTO;
using EvaluationAPI.DAL.Entities;
using EvaluationAPI.BLL.Common;

namespace EvaluationAPI.BLL.Contracts
{
    public interface IEvaluationService
    {
        Task<IPagedResponse<TestDTO>> GetTestsSummaryAsync(int pageSize = 10, int pageNumber = 1);

        Task<ISingleResponse<TestDTO>> GetTestAsync(long id);

        Task<IPagedResponse<ResultDTO>> GetResultsByTestAsync(long testId);

        Task<IPagedResponse<ResultDTO>> GetResultsByUserAsync(string userName);

        Task<ISingleResponse<ResultDTO>> AddResultForTestAsync(long testId, string userName, ResultDTO result);
    }
}
