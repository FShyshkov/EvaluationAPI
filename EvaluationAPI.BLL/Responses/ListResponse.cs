using EvaluationAPI.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationAPI.BLL.Responses
{
    public class ListResponse<TModel> : IListResponse<TModel>
    {
        public string Message { get; set; }

        public bool ErrorOccured { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<TModel> Model { get; set; }
    }

}
