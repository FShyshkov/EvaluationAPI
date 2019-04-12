using System;
using System.Collections.Generic;
using System.Text;
using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.BLL.Exceptions;

namespace EvaluationAPI.BLL.Responses
{
    public static class ResponseExtensions
    {
        public static void SetError(this IResponse response, string actionName, Exception ex)
        {

            response.ErrorOccured = true;

            if (ex is EvaluationException cast)
            {
                response.ErrorMessage = ex.Message;
            }
            else  {
               

                response.ErrorMessage = "There was an internal error, please contact to technical support.";
            }
        }
    }
}
