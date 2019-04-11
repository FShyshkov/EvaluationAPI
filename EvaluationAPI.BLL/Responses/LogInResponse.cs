using System.Collections.Generic;
using EvaluationAPI.BLL.Contracts;
using System;
using EvaluationAPI.DAL.Identity.Responses;
using EvaluationAPI.BLL.DTO;

namespace EvaluationAPI.BLL.Responses
{
    public class LoginResponse
    {
        public bool Success { get; }
        public string Message { get; }       
        public AccessToken AccessToken { get; }
        public IEnumerable<Error> Errors { get; }

        public LoginResponse(IEnumerable<Error> errors, bool success = false, string message = null)
        {
            Errors = errors;
            Success = success;
            Message = message;
        }

        public LoginResponse(AccessToken accessToken, bool success = false, string message = null)
        {
            AccessToken = accessToken;
            Success = success;
            Message = message;
        }
    }
}
