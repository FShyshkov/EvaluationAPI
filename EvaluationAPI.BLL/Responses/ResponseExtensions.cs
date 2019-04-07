using System;
using System.Collections.Generic;
using System.Text;
using EvaluationAPI.BLL.Contracts;

namespace EvaluationAPI.BLL.Responses
{
    public static class ResponseExtensions
    {
        public static void SetError(this IResponse response, string actionName, Exception ex)
        {
            // todo: Save error in log file

            response.ErrorOccured = true;

            if (ex is Exception cast)
            {
                response.ErrorMessage = ex.Message;
            }
            else            {
               

                response.ErrorMessage = "There was an internal error, please contact to technical support.";
            }
        }
    }
}
