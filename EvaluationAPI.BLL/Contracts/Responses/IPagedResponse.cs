using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationAPI.BLL.Contracts
{
    public interface IPagedResponse<TModel> : IListResponse<TModel>
    {
        int ItemsCount { get; set; }

        double PageCount { get; }
    }
}
