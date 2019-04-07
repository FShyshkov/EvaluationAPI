using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationAPI.BLL.Contracts
{
    public interface IResponse
    {
        string Message { get; set; }

        bool ErrorOccured { get; set; }

        string ErrorMessage { get; set; }
    }
}
