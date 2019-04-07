using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationAPI.BLL.Contracts
{
    public interface ISingleResponse<TModel> : IResponse
    {
        TModel Model { get; set; }
    }
}
