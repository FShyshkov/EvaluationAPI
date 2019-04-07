using System.Collections.Generic;

namespace EvaluationAPI.BLL.Contracts
{    
        public interface IListResponse<TModel> : IResponse
        {
            IEnumerable<TModel> Model { get; set; }
        }
}
